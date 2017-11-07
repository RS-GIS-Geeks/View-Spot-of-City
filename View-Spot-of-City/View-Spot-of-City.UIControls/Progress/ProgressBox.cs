using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace View_Spot_of_City.UIControls.Progress
{
    /// <summary>
    /// 进度条对话框
    /// </summary>
    public class ProgressBox
    {
        private FromProgressBox m_frmProgressBox = null;
        private Thread m_thread = null;
        private string m_Title = "";
        private double m_MaxValue = 100;
        private double m_MinValue = 0;

        /// <summary>
        /// 构造
        /// </summary>
        public ProgressBox()
        {
        }

        public ProgressBox(int Min, int Max, string vTitle)
        {
            MaxValue = Max;
            MinValue = Min;
            Title = vTitle;
        }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            get { return m_Title; }
            set { m_Title = value; }
        }

        /// <summary>
        /// 最大值
        /// </summary>
        public double MaxValue
        {
            get { return m_MaxValue; }
            set { m_MaxValue = value; }
        }

        /// <summary>
        /// 最小值
        /// </summary>
        public double MinValue
        {
            get { return m_MinValue; }
            set { m_MinValue = value; }
        }

        /// <summary>
        /// 显示进度条
        /// </summary>
        public void ShowPregress()
        {
            m_thread = new Thread(Show)
            {
                IsBackground = true
            };
            m_thread.SetApartmentState(ApartmentState.STA);
            m_thread.Start();
        }

        private void Show()
        {
            m_frmProgressBox = new FromProgressBox()
            {
                Caption = m_Title,
                MaxValue = m_MaxValue,
                MinValue = m_MinValue
            };
            m_frmProgressBox.ShowDialog();
        }

        /// <summary>
        /// 关闭进度条
        /// </summary>
        public void CloseProgress()
        {
            while (!m_thread.IsAlive) ;
            Thread.Sleep(1);
            m_frmProgressBox.RequestStop();
            m_thread.Join();
        }

        /// <summary>
        /// 设置进度条当前值
        /// </summary>
        /// <param name="nValue"></param>
        public void SetProgressValue(double nValue)
        {
            while(m_frmProgressBox == null)
            { }
            m_frmProgressBox.SetProgressValue(nValue);
        }

        /// <summary>
        /// 设置进度条当前描述信息
        /// </summary>
        /// <param name="strValue"></param>
        public void SetProgressDescription(string strValue)
        {
            while (m_frmProgressBox == null)
            { }
            m_frmProgressBox.SetProgressText(strValue);
        }

        public void SetProgressDescription2(string strValue)
        {
            while (m_frmProgressBox == null)
            { }
            m_frmProgressBox.SetProgressText2(strValue);
        }
    }
}
