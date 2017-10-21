using Esri.ArcGISRuntime.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace View_Spot_of_City.ArcGISControls
{
    /// <summary>
    /// SceneView.xaml 的交互逻辑
    /// </summary>
    public partial class SceneView : UserControl
    {
        public SceneView()
        {
            InitializeComponent();

            Initialize();
        }

        private void Initialize()
        {
            // Create new Scene
            Scene myScene = new Scene()
            {

                // Set Scene's base map property
                Basemap = Basemap.CreateImagery()
            };

            // Create uri to the scene layer
            var serviceUri = new Uri(
               "https://scene.arcgis.com/arcgis/rest/services/Hosted/Buildings_Brest/SceneServer/0");

            // Create new scene layer from the url
            ArcGISSceneLayer sceneLayer = new ArcGISSceneLayer(serviceUri);

            // Add created layer to the operational layers collection
            myScene.OperationalLayers.Add(sceneLayer);

            // Create a camera with coordinates showing layer data 
            Camera camera = new Camera(48.378, -4.494, 200, 345, 65, 0);

            // Assign the Scene to the SceneView
            MySceneView.Scene = myScene;

            // Set view point of scene view using camera 
            //MySceneView.SetViewpointCameraAsync(camera);

        }
    }
}
