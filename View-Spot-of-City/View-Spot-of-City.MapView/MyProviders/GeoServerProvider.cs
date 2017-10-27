
namespace GMap.NET.MapProviders
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.Xml;
    using Internals;
    using GMap.NET.Projections;
    using System.Net;
    using System.IO;
    using System.Windows;
    using RestSharp;
    using System.Threading.Tasks;

    using Config = System.Configuration.ConfigurationManager;
    using System.Text;

    public abstract class GeoServerProviderBase : GMapProvider, RoutingProvider, GeocodingProvider
    {
        public GeoServerProviderBase()
        {
            MaxZoom = null;
            RefererUrl = null;
            Copyright = string.Format("© GeoServer - OpenStreetMap data ©{0} RS-GIS-Geeks", DateTime.Today.Year);
        }

        public readonly string ServerLetters = null;
        public int MinExpectedRank = 0;

        #region GMapProvider Members

        public override Guid Id
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override string Name
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override PureProjection Projection
        {
            get
            {
                return MercatorProjection.Instance;
            }
        }

        public override GMapProvider[] Overlays
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override PureImage GetTileImage(GPoint pos, int zoom)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region GMapRoutingProvider Members

        public MapRoute GetRoute(PointLatLng start, PointLatLng end, bool avoidHighways, bool walkingMode, int Zoom)
        {
            List<PointLatLng> points = GetRoutePoints(MakeRoutingUrl(start, end, walkingMode ? TravelTypeFoot : TravelTypeMotorCar));
            MapRoute route = points != null ? new MapRoute(points, walkingMode ? WalkingStr : DrivingStr) : null;
            return route;
        }

        /// <summary>
        /// NotImplemented
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="avoidHighways"></param>
        /// <param name="walkingMode"></param>
        /// <param name="Zoom"></param>
        /// <returns></returns>
        public MapRoute GetRoute(string start, string end, bool avoidHighways, bool walkingMode, int Zoom)
        {
            throw new NotImplementedException("use GetRoute(PointLatLng start, PointLatLng end...");
        }

        #region -- internals --
        string MakeRoutingUrl(PointLatLng start, PointLatLng end, string travelType)
        {
            return string.Format(CultureInfo.InvariantCulture, RoutingUrlFormat, start.Lat, start.Lng, end.Lat, end.Lng, travelType);
        }

        List<PointLatLng> GetRoutePoints(string url)
        {
            List<PointLatLng> points = null;
            try
            {
                string route = GMaps.Instance.UseRouteCache ? Cache.Instance.GetContent(url, CacheType.RouteCache) : string.Empty;
                if (string.IsNullOrEmpty(route))
                {
                    route = GetContentUsingHttp(url);
                    if (!string.IsNullOrEmpty(route))
                    {
                        if (GMaps.Instance.UseRouteCache)
                        {
                            Cache.Instance.SaveContent(url, CacheType.RouteCache, route);
                        }
                    }
                }

                if (!string.IsNullOrEmpty(route))
                {
                    XmlDocument xmldoc = new XmlDocument();
                    xmldoc.LoadXml(route);
                    System.Xml.XmlNamespaceManager xmlnsManager = new System.Xml.XmlNamespaceManager(xmldoc.NameTable);
                    xmlnsManager.AddNamespace(/*"sm"*/null, /*"http://earth.google.com/kml/2.0"*/null);

                    ///Folder/Placemark/LineString/coordinates
                    var coordNode = xmldoc.SelectSingleNode("/sm:kml/sm:Document/sm:Folder/sm:Placemark/sm:LineString/sm:coordinates", xmlnsManager);

                    string[] coordinates = coordNode.InnerText.Split('\n');

                    if (coordinates.Length > 0)
                    {
                        points = new List<PointLatLng>();

                        foreach (string coordinate in coordinates)
                        {
                            if (coordinate != string.Empty)
                            {
                                string[] XY = coordinate.Split(',');
                                if (XY.Length == 2)
                                {
                                    double lat = double.Parse(XY[1], CultureInfo.InvariantCulture);
                                    double lng = double.Parse(XY[0], CultureInfo.InvariantCulture);
                                    points.Add(new PointLatLng(lat, lng));
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("GetRoutePoints: " + ex);
            }

            return points;
        }

        static readonly string RoutingUrlFormat = /*"http://www.yournavigation.org/api/1.0/gosmore.php?format=kml&flat={0}&flon={1}&tlat={2}&tlon={3}&v={4}&fast=1&layer=mapnik"*/null;
        static readonly string TravelTypeFoot = "foot";
        static readonly string TravelTypeMotorCar = "motorcar";

        static readonly string WalkingStr = "Walking";
        static readonly string DrivingStr = "Driving";
        #endregion

        #endregion

        #region GeocodingProvider Members

        public GeoCoderStatusCode GetPoints(string keywords, out List<PointLatLng> pointList)
        {
            // http://nominatim.openstreetmap.org/search?q=lithuania,vilnius&format=xml

            #region -- response --
            //<searchresults timestamp="Wed, 01 Feb 12 09:46:00 -0500" attribution="Data Copyright OpenStreetMap Contributors, Some Rights Reserved. CC-BY-SA 2.0." querystring="lithuania,vilnius" polygon="false" exclude_place_ids="29446018,53849547,8831058,29614806" more_url="http://open.mapquestapi.com/nominatim/v1/search?format=xml&exclude_place_ids=29446018,53849547,8831058,29614806&accept-language=en&q=lithuania%2Cvilnius">
            //<place place_id="29446018" osm_type="way" osm_id="24598347" place_rank="30" boundingbox="54.6868133544922,54.6879043579102,25.2885360717773,25.2898139953613" lat="54.6873633486028" lon="25.289199818878" display_name="National Museum of Lithuania, 1, Arsenalo g., Senamiesčio seniūnija, YAHOO-HIRES-20080313, Vilnius County, Šalčininkų rajonas, Vilniaus apskritis, 01513, Lithuania" class="tourism" type="museum" icon="http://open.mapquestapi.com/nominatim/v1/images/mapicons/tourist_museum.p.20.png"/>
            //<place place_id="53849547" osm_type="way" osm_id="55469274" place_rank="30" boundingbox="54.6896553039551,54.690486907959,25.2675743103027,25.2692089080811" lat="54.6900227236882" lon="25.2683589759401" display_name="Ministry of Foreign Affairs of the Republic of Lithuania, 2, J. Tumo Vaižganto g., Naujamiesčio seniūnija, Vilnius, Vilnius County, Vilniaus m. savivaldybė, Vilniaus apskritis, LT-01104, Lithuania" class="amenity" type="public_building"/>
            //<place place_id="8831058" osm_type="node" osm_id="836234960" place_rank="30" boundingbox="54.6670935059,54.6870973206,25.2638857269,25.2838876343" lat="54.677095" lon="25.2738876" display_name="Railway Museum of Lithuania, 15, Mindaugo g., Senamiesčio seniūnija, Vilnius, Vilnius County, Vilniaus m. savivaldybė, Vilniaus apskritis, 03215, Lithuania" class="tourism" type="museum" icon="http://open.mapquestapi.com/nominatim/v1/images/mapicons/tourist_museum.p.20.png"/>
            //<place place_id="29614806" osm_type="way" osm_id="24845629" place_rank="30" boundingbox="54.6904983520508,54.6920852661133,25.2606296539307,25.2628803253174" lat="54.6913385159005" lon="25.2617684209873" display_name="Seimas (Parliament) of the Republic of Lithuania, 53, Gedimino pr., Naujamiesčio seniūnija, Vilnius, Vilnius County, Vilniaus m. savivaldybė, Vilniaus apskritis, LT-01111, Lithuania" class="amenity" type="public_building"/>
            //</searchresults> 
            #endregion

            return GetLatLngFromGeocoderUrl(MakeGeocoderUrl(keywords), out pointList);
        }

        public PointLatLng? GetPoint(string keywords, out GeoCoderStatusCode status)
        {
            List<PointLatLng> pointList;
            status = GetPoints(keywords, out pointList);
            return pointList != null && pointList.Count > 0 ? pointList[0] : (PointLatLng?)null;
        }

        public GeoCoderStatusCode GetPoints(Placemark placemark, out List<PointLatLng> pointList)
        {
            // http://nominatim.openstreetmap.org/search?street=&city=vilnius&county=&state=&country=lithuania&postalcode=&format=xml

            #region -- response --
            //<searchresults timestamp="Thu, 29 Nov 12 08:38:23 +0000" attribution="Data © OpenStreetMap contributors, ODbL 1.0. http://www.openstreetmap.org/copyright" querystring="vilnius, lithuania" polygon="false" exclude_place_ids="98093941" more_url="http://nominatim.openstreetmap.org/search?format=xml&exclude_place_ids=98093941&accept-language=de-de,de;q=0.8,en-us;q=0.5,en;q=0.3&q=vilnius%2C+lithuania">
            //<place place_id="98093941" osm_type="relation" osm_id="1529146" place_rank="16" boundingbox="54.5693359375,54.8323097229004,25.0250644683838,25.4815216064453" lat="54.6843135" lon="25.2853984" display_name="Vilnius, Vilniaus m. savivaldybė, Distrikt Vilnius, Litauen" class="boundary" type="administrative" icon="http://nominatim.openstreetmap.org/images/mapicons/poi_boundary_administrative.p.20.png"/>
            //</searchresults> 
            #endregion

            return GetLatLngFromGeocoderUrl(MakeDetailedGeocoderUrl(placemark), out pointList);
        }

        public PointLatLng? GetPoint(Placemark placemark, out GeoCoderStatusCode status)
        {
            List<PointLatLng> pointList;
            status = GetPoints(placemark, out pointList);
            return pointList != null && pointList.Count > 0 ? pointList[0] : (PointLatLng?)null;
        }

        public GeoCoderStatusCode GetPlacemarks(PointLatLng location, out List<Placemark> placemarkList)
        {
            throw new NotImplementedException("use GetPlacemark");
        }

        public Placemark? GetPlacemark(PointLatLng location, out GeoCoderStatusCode status)
        {
            //http://nominatim.openstreetmap.org/reverse?format=xml&lat=52.5487429714954&lon=-1.81602098644987&zoom=18&addressdetails=1

            #region -- response --
            /*
            <reversegeocode timestamp="Wed, 01 Feb 12 09:51:11 -0500" attribution="Data Copyright OpenStreetMap Contributors, Some Rights Reserved. CC-BY-SA 2.0." querystring="format=xml&lat=52.5487429714954&lon=-1.81602098644987&zoom=18&addressdetails=1">
            <result place_id="2061235282" osm_type="way" osm_id="90394420" lat="52.5487800131654" lon="-1.81626922291265">
            137, Pilkington Avenue, Castle Vale, City of Birmingham, West Midlands, England, B72 1LH, United Kingdom
            </result>
            <addressparts>
            <house_number>
            137
            </house_number>
            <road>
            Pilkington Avenue
            </road>
            <suburb>
            Castle Vale
            </suburb>
            <city>
            City of Birmingham
            </city>
            <county>
            West Midlands
            </county>
            <state_district>
            West Midlands
            </state_district>
            <state>
            England
            </state>
            <postcode>
            B72 1LH
            </postcode>
            <country>
            United Kingdom
            </country>
            <country_code>
            gb
            </country_code>
            </addressparts>
            </reversegeocode>
            */

            #endregion

            return GetPlacemarkFromReverseGeocoderUrl(MakeReverseGeocoderUrl(location), out status);
        }

        #region -- internals --

        string MakeGeocoderUrl(string keywords)
        {
            return string.Format(GeocoderUrlFormat, keywords.Replace(' ', '+'));
        }

        string MakeDetailedGeocoderUrl(Placemark placemark)
        {
            var street = String.Join(" ", new[] { placemark.HouseNo, placemark.ThoroughfareName }).Trim();
            return string.Format(GeocoderDetailedUrlFormat,
                                 street.Replace(' ', '+'),
                                 placemark.LocalityName.Replace(' ', '+'),
                                 placemark.SubAdministrativeAreaName.Replace(' ', '+'),
                                 placemark.AdministrativeAreaName.Replace(' ', '+'),
                                 placemark.CountryName.Replace(' ', '+'),
                                 placemark.PostalCodeNumber.Replace(' ', '+'));
        }

        string MakeReverseGeocoderUrl(PointLatLng pt)
        {
            return string.Format(CultureInfo.InvariantCulture, ReverseGeocoderUrlFormat, pt.Lat, pt.Lng);
        }

        GeoCoderStatusCode GetLatLngFromGeocoderUrl(string url, out List<PointLatLng> pointList)
        {
            var status = GeoCoderStatusCode.Unknow;
            pointList = null;

            try
            {
                string geo = GMaps.Instance.UseGeocoderCache ? Cache.Instance.GetContent(url, CacheType.GeocoderCache) : string.Empty;

                bool cache = false;

                if (string.IsNullOrEmpty(geo))
                {
                    geo = GetContentUsingHttp(url);

                    if (!string.IsNullOrEmpty(geo))
                    {
                        cache = true;
                    }
                }

                if (!string.IsNullOrEmpty(geo))
                {
                    if (geo.StartsWith("<?xml") && geo.Contains("<place"))
                    {
                        if (cache && GMaps.Instance.UseGeocoderCache)
                        {
                            Cache.Instance.SaveContent(url, CacheType.GeocoderCache, geo);
                        }

                        XmlDocument doc = new XmlDocument();
                        doc.LoadXml(geo);
                        {
                            XmlNodeList l = doc.SelectNodes("/searchresults/place");
                            if (l != null)
                            {
                                pointList = new List<PointLatLng>();

                                foreach (XmlNode n in l)
                                {
                                    var nn = n.Attributes["place_rank"];

                                    int rank = 0;
#if !PocketPC
                                    if (nn != null && Int32.TryParse(nn.Value, out rank))
                                    {
#else
                           if(nn != null && !string.IsNullOrEmpty(nn.Value))
                           {
                              rank = int.Parse(nn.Value, NumberStyles.Integer, CultureInfo.InvariantCulture);
#endif
                                        if (rank < MinExpectedRank)
                                            continue;
                                    }

                                    nn = n.Attributes["lat"];
                                    if (nn != null)
                                    {
                                        double lat = double.Parse(nn.Value, CultureInfo.InvariantCulture);

                                        nn = n.Attributes["lon"];
                                        if (nn != null)
                                        {
                                            double lng = double.Parse(nn.Value, CultureInfo.InvariantCulture);
                                            pointList.Add(new PointLatLng(lat, lng));
                                        }
                                    }
                                }

                                status = GeoCoderStatusCode.G_GEO_SUCCESS;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                status = GeoCoderStatusCode.ExceptionInCode;
                Debug.WriteLine("GetLatLngFromGeocoderUrl: " + ex);
            }

            return status;
        }

        Placemark? GetPlacemarkFromReverseGeocoderUrl(string url, out GeoCoderStatusCode status)
        {
            status = GeoCoderStatusCode.Unknow;
            Placemark? ret = null;

            try
            {
                string geo = GMaps.Instance.UsePlacemarkCache ? Cache.Instance.GetContent(url, CacheType.PlacemarkCache) : string.Empty;

                bool cache = false;

                if (string.IsNullOrEmpty(geo))
                {
                    geo = GetContentUsingHttp(url);

                    if (!string.IsNullOrEmpty(geo))
                    {
                        cache = true;
                    }
                }

                if (!string.IsNullOrEmpty(geo))
                {
                    if (geo.StartsWith("<?xml") && geo.Contains("<result"))
                    {
                        if (cache && GMaps.Instance.UsePlacemarkCache)
                        {
                            Cache.Instance.SaveContent(url, CacheType.PlacemarkCache, geo);
                        }

                        XmlDocument doc = new XmlDocument();
                        doc.LoadXml(geo);
                        {
                            XmlNode r = doc.SelectSingleNode("/reversegeocode/result");
                            if (r != null)
                            {
                                var p = new Placemark(r.InnerText);

                                XmlNode ad = doc.SelectSingleNode("/reversegeocode/addressparts");
                                if (ad != null)
                                {
                                    var vl = ad.SelectSingleNode("country");
                                    if (vl != null)
                                    {
                                        p.CountryName = vl.InnerText;
                                    }

                                    vl = ad.SelectSingleNode("country_code");
                                    if (vl != null)
                                    {
                                        p.CountryNameCode = vl.InnerText;
                                    }

                                    vl = ad.SelectSingleNode("postcode");
                                    if (vl != null)
                                    {
                                        p.PostalCodeNumber = vl.InnerText;
                                    }

                                    vl = ad.SelectSingleNode("state");
                                    if (vl != null)
                                    {
                                        p.AdministrativeAreaName = vl.InnerText;
                                    }

                                    vl = ad.SelectSingleNode("region");
                                    if (vl != null)
                                    {
                                        p.SubAdministrativeAreaName = vl.InnerText;
                                    }

                                    vl = ad.SelectSingleNode("suburb");
                                    if (vl != null)
                                    {
                                        p.LocalityName = vl.InnerText;
                                    }

                                    vl = ad.SelectSingleNode("road");
                                    if (vl != null)
                                    {
                                        p.ThoroughfareName = vl.InnerText;
                                    }
                                }

                                ret = p;

                                status = GeoCoderStatusCode.G_GEO_SUCCESS;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ret = null;
                status = GeoCoderStatusCode.ExceptionInCode;
                Debug.WriteLine("GetPlacemarkFromReverseGeocoderUrl: " + ex);
            }

            return ret;
        }

        static readonly string ReverseGeocoderUrlFormat = /*"http://nominatim.openstreetmap.org/reverse?format=xml&lat={0}&lon={1}&zoom=18&addressdetails=1"*/null;
        static readonly string GeocoderUrlFormat = /*"http://nominatim.openstreetmap.org/search?q={0}&format=xml"*/null;
        static readonly string GeocoderDetailedUrlFormat = /*"http://nominatim.openstreetmap.org/search?street={0}&city={1}&county={2}&state={3}&country={4}&postalcode={5}&format=xml"*/null;

        #endregion

        #endregion
    }

    /// <summary>
    /// OpenStreetMap provider - http://www.openstreetmap.org/
    /// </summary>
    public class GeoServerProvider : GeoServerProviderBase
    {
        public static readonly GeoServerProvider Instance;

        public GeoServerProvider()
        {
        }

        static GeoServerProvider()
        {
            Instance = new GeoServerProvider();
        }

        #region GMapProvider Members

        readonly Guid id = Guid.NewGuid();
        public override Guid Id
        {
            get
            {
                return id;
            }
        }

        readonly string name = "ChinaMap";
        public override string Name
        {
            get
            {
                return name;
            }
        }

        GMapProvider[] overlays;
        public override GMapProvider[] Overlays
        {
            get
            {
                if (overlays == null)
                {
                    overlays = new GMapProvider[] { this };
                }
                return overlays;
            }
        }

        public override PureImage GetTileImage(GPoint pos, int zoom)
        {
            string url = MakeTileImageUrl(pos, zoom, string.Empty);

            return GetTileImageUsingHttp(url);
        }

        #endregion

        /***********************************************************************************************************************************************************/
        /***********************************************************************************************************************************************************/
        #region GeoSever Interface

        /// <summary>
        /// 网络状态，为true则网络正常
        /// </summary>
        public bool networkState = true;

        /// <summary>
        /// 服务url
        /// </summary>
        public static readonly string serverHostPort = Config.AppSettings["WEB_API_HOST_PORT"];

        #region xmlFormat
        public static readonly string xmlFormatForGetArea = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n<wps:Execute version=\"1.0.0\" service=\"WPS\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns=\"http://www.opengis.net/wps/1.0.0\" xmlns:wfs=\"http://www.opengis.net/wfs\" xmlns:wps=\"http://www.opengis.net/wps/1.0.0\" xmlns:ows=\"http://www.opengis.net/ows/1.1\" xmlns:gml=\"http://www.opengis.net/gml\" xmlns:ogc=\"http://www.opengis.net/ogc\" xmlns:wcs=\"http://www.opengis.net/wcs/1.1.1\" xmlns:xlink=\"http://www.w3.org/1999/xlink\" xsi:schemaLocation=\"http://www.opengis.net/wps/1.0.0 http://schemas.opengis.net/wps/1.0.0/wpsAll.xsd\">\r\n    <ows:Identifier>JTS:area</ows:Identifier>\r\n    <wps:DataInputs>\r\n        <wps:Input>\r\n            <ows:Identifier>geom</ows:Identifier>\r\n            <wps:Data>\r\n                <wps:ComplexData mimeType=\"text/xml; subtype=gml/3.1.1\">\r\n                    <gml:Polygon xmlns:gml=\"http://www.opengis.net/gml\" xmlns:sch=\"http://www.ascc.net/xml/schematron\" xmlns:xlink=\"http://www.w3.org/1999/xlink\" srsDimension=\"2\">\r\n                        <gml:exterior>\r\n                            <gml:LinearRing srsDimension=\"2\">\r\n                                <gml:posList>{0}</gml:posList>\r\n                            </gml:LinearRing>\r\n                        </gml:exterior>\r\n                    </gml:Polygon>\r\n                </wps:ComplexData>\r\n            </wps:Data>\r\n        </wps:Input>\r\n    </wps:DataInputs>\r\n    <wps:ResponseForm>\r\n        <wps:RawDataOutput>\r\n            <ows:Identifier>result</ows:Identifier>\r\n        </wps:RawDataOutput>\r\n    </wps:ResponseForm>\r\n</wps:Execute>";

        public static readonly string xmlFormatForLatLngToCartesian = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><wps:Execute version=\"1.0.0\" service=\"WPS\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns=\"http://www.opengis.net/wps/1.0.0\" xmlns:wfs=\"http://www.opengis.net/wfs\" xmlns:wps=\"http://www.opengis.net/wps/1.0.0\" xmlns:ows=\"http://www.opengis.net/ows/1.1\" xmlns:gml=\"http://www.opengis.net/gml\" xmlns:ogc=\"http://www.opengis.net/ogc\" xmlns:wcs=\"http://www.opengis.net/wcs/1.1.1\" xmlns:xlink=\"http://www.w3.org/1999/xlink\" xsi:schemaLocation=\"http://www.opengis.net/wps/1.0.0 http://schemas.opengis.net/wps/1.0.0/wpsAll.xsd\">\r\n  <ows:Identifier>geo:reproject</ows:Identifier>\r\n  <wps:DataInputs>\r\n    <wps:Input>\r\n      <ows:Identifier>geometry</ows:Identifier>\r\n      <wps:Data>\r\n        <wps:ComplexData mimeType=\"text/xml; subtype=gml/3.1.1\"><![CDATA[{0}({1})]]></wps:ComplexData>\r\n      </wps:Data>\r\n    </wps:Input>\r\n    <wps:Input>\r\n      <ows:Identifier>sourceCRS</ows:Identifier>\r\n      <wps:Data>\r\n        <wps:LiteralData>EPSG:4326</wps:LiteralData>\r\n      </wps:Data>\r\n    </wps:Input>\r\n    <wps:Input>\r\n      <ows:Identifier>targetCRS</ows:Identifier>\r\n      <wps:Data>\r\n        <wps:LiteralData>EPSG:900913</wps:LiteralData>\r\n      </wps:Data>\r\n    </wps:Input>\r\n  </wps:DataInputs>\r\n  <wps:ResponseForm>\r\n    <wps:RawDataOutput mimeType=\"text/xml; subtype=gml/3.1.1\">\r\n      <ows:Identifier>result</ows:Identifier>\r\n    </wps:RawDataOutput>\r\n  </wps:ResponseForm>\r\n</wps:Execute>";

        public static readonly string xmlFormatForCartesianToLatLng = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><wps:Execute version=\"1.0.0\" service=\"WPS\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns=\"http://www.opengis.net/wps/1.0.0\" xmlns:wfs=\"http://www.opengis.net/wfs\" xmlns:wps=\"http://www.opengis.net/wps/1.0.0\" xmlns:ows=\"http://www.opengis.net/ows/1.1\" xmlns:gml=\"http://www.opengis.net/gml\" xmlns:ogc=\"http://www.opengis.net/ogc\" xmlns:wcs=\"http://www.opengis.net/wcs/1.1.1\" xmlns:xlink=\"http://www.w3.org/1999/xlink\" xsi:schemaLocation=\"http://www.opengis.net/wps/1.0.0 http://schemas.opengis.net/wps/1.0.0/wpsAll.xsd\">\r\n  <ows:Identifier>geo:reproject</ows:Identifier>\r\n  <wps:DataInputs>\r\n    <wps:Input>\r\n      <ows:Identifier>geometry</ows:Identifier>\r\n      <wps:Data>\r\n        <wps:ComplexData mimeType=\"text/xml; subtype=gml/3.1.1\"><![CDATA[{0}({1})]]></wps:ComplexData>\r\n      </wps:Data>\r\n    </wps:Input>\r\n    <wps:Input>\r\n      <ows:Identifier>sourceCRS</ows:Identifier>\r\n      <wps:Data>\r\n        <wps:LiteralData>EPSG:900913</wps:LiteralData>\r\n      </wps:Data>\r\n    </wps:Input>\r\n    <wps:Input>\r\n      <ows:Identifier>targetCRS</ows:Identifier>\r\n      <wps:Data>\r\n        <wps:LiteralData>EPSG:4326</wps:LiteralData>\r\n      </wps:Data>\r\n    </wps:Input>\r\n  </wps:DataInputs>\r\n  <wps:ResponseForm>\r\n    <wps:RawDataOutput mimeType=\"text/xml; subtype=gml/3.1.1\">\r\n      <ows:Identifier>result</ows:Identifier>\r\n    </wps:RawDataOutput>\r\n  </wps:ResponseForm>\r\n</wps:Execute>";

        public static readonly string xmlFormatForGetBufferCircle = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><wps:Execute version=\"1.0.0\" service=\"WPS\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns=\"http://www.opengis.net/wps/1.0.0\" xmlns:wfs=\"http://www.opengis.net/wfs\" xmlns:wps=\"http://www.opengis.net/wps/1.0.0\" xmlns:ows=\"http://www.opengis.net/ows/1.1\" xmlns:gml=\"http://www.opengis.net/gml\" xmlns:ogc=\"http://www.opengis.net/ogc\" xmlns:wcs=\"http://www.opengis.net/wcs/1.1.1\" xmlns:xlink=\"http://www.w3.org/1999/xlink\" xsi:schemaLocation=\"http://www.opengis.net/wps/1.0.0 http://schemas.opengis.net/wps/1.0.0/wpsAll.xsd\">\r\n  <ows:Identifier>JTS:buffer</ows:Identifier>\r\n  <wps:DataInputs>\r\n    <wps:Input>\r\n      <ows:Identifier>geom</ows:Identifier>\r\n      <wps:Data>\r\n        <wps:ComplexData mimeType=\"text/xml; subtype=gml/3.1.1\"><![CDATA[point({0})]]></wps:ComplexData>\r\n      </wps:Data>\r\n    </wps:Input>\r\n    <wps:Input>\r\n      <ows:Identifier>distance</ows:Identifier>\r\n      <wps:Data>\r\n        <wps:LiteralData>{1}</wps:LiteralData>\r\n      </wps:Data>\r\n    </wps:Input>\r\n  </wps:DataInputs>\r\n  <wps:ResponseForm>\r\n    <wps:RawDataOutput mimeType=\"text/xml; subtype=gml/3.1.1\">\r\n      <ows:Identifier>result</ows:Identifier>\r\n    </wps:RawDataOutput>\r\n  </wps:ResponseForm>\r\n</wps:Execute>";
        
        public static readonly string shortestPathUrl = Config.AppSettings["ShortestPathServerApi"] + "shortpath";
        #endregion

        string MakeTileImageUrl(GPoint pos, int zoom, string language)
        {
            string str = string.Format(UrlFormat, zoom, pos.X, pos.Y);
            return str;
        }

        /// <summary>
        /// 切片url
        /// </summary>
        static readonly string UrlFormat = Config.AppSettings["WMTS_HOST_PORT"] + @"geoserver/gwc/service/wmts?layer=ShenZhenMap%3Ashenzhen&style&tilematrixset=EPSG%3A900913&Service=WMTS&Request=GetTile&Version=1.0.0&Format=image%2Fpng&TileMatrix=EPSG%3A900913%3A{0}&TileCol={1}&TileRow={2}";

        ///<summary>
        ///采用https协议访问网络
        ///</summary>
        ///<param name="URL">url地址</param>
        ///<param name="strPostdata">发送的数据</param>
        ///<returns>返回的数据</returns>
        public Task<IRestResponse> GetHttpResponseAsyncAsync(string URL, string strPostdata, Method method = Method.POST)
        {
            var client = new RestClient(URL);
            var request = new RestRequest(method);
            request.Timeout = Convert.ToInt32(Config.AppSettings["NET_WORK_DELAY"]);
            request.AddHeader("cache-control", "no-cache");
            request.AddParameter("undefined", strPostdata, ParameterType.RequestBody);
            return client.ExecutePostTaskAsync(request);
        }
        
        /// <summary>
        /// 通过访问服务获得两点间的最短路径
        /// </summary>
        /// <param name="startPll">起点</param>
        /// <param name="endPll">终点</param>
        /// <returns>路径的结点和节点组成的链表</returns>
        public async Task<List<PointLatLng>> GetShortestRouteAsync(PointLatLng startPll, PointLatLng endPll)
        {
            try
            {
                string requestUrl = shortestPathUrl + "?x1=" + startPll.Lng + "&y1=" + startPll.Lat + "&x2=" + endPll.Lng + "&y2=" + endPll.Lat;

                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(requestUrl);

                httpWebRequest.ContentType = "application/xml";
                httpWebRequest.Method = "GET";
                //对发送的数据不使用缓存
                httpWebRequest.AllowWriteStreamBuffering = false;
                httpWebRequest.Timeout = 300000;
                httpWebRequest.ServicePoint.Expect100Continue = false;

                HttpWebResponse webRespon = (HttpWebResponse)(await httpWebRequest.GetResponseAsync());
                Stream webStream = webRespon.GetResponseStream();
                StreamReader streamReader = new StreamReader(webStream, Encoding.UTF8);
                string responseContent = streamReader.ReadToEnd();

                webRespon.Close();
                streamReader.Close();
                List<PointLatLng> pllList = new List<PointLatLng>();
                {
                    XmlDataDocument xmlDataDoc = new XmlDataDocument();
                    xmlDataDoc.LoadXml(responseContent);
                    XmlNode root = xmlDataDoc.SelectSingleNode("Route");
                    XmlNodeList childlist = root.ChildNodes;
                    string cost = childlist[0].InnerText;
                    XmlNodeList pointsList = childlist[1].ChildNodes;
                    PointLatLng tempPoint = new PointLatLng();
                    foreach (XmlNode pointNode in pointsList)
                    {
                        tempPoint.Lng = Convert.ToDouble(pointNode.SelectSingleNode("x").InnerText);
                        tempPoint.Lat = Convert.ToDouble(pointNode.SelectSingleNode("y").InnerText);
                        pllList.Add(tempPoint);
                    }
                }
                networkState = true;
                return pllList;
            }
            catch (Exception ex)
            {
                networkState = false;
                return new List<PointLatLng>();
            }
        }
        #endregion
        /***********************************************************************************************************************************************************/
        /***********************************************************************************************************************************************************/
    }
}
