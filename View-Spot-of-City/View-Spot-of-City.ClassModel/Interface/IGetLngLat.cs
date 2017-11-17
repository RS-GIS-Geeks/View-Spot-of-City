namespace View_Spot_of_City.ClassModel.Interface
{
    /// <summary>
    /// 获取经纬度接口
    /// </summary>
    public interface IGetLngLat
    {
        /// <summary>
        /// 返回经度
        /// </summary>
        /// <returns>经度</returns>
        double GetLng();

        /// <summary>
        /// 返回纬度
        /// </summary>
        /// <returns>纬度</returns>
        double GetLat();
    }
}
