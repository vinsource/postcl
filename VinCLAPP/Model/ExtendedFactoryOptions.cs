namespace VinCLAPP.Model
{
    public class ExtendedFactoryOptions
    {
        private string _mCategoryName;
        private bool _mInstalled;
        private string _mMsrp;
        private string _mName;
        private bool _mStandard;
        public string Description { get; set; }

        public string GetCategoryName()
        {
            return _mCategoryName;
        }

        public void SetCategoryName(string mCategoryName)
        {
            _mCategoryName = mCategoryName;
        }

        public string GetMsrp()
        {
            return _mMsrp;
        }

        public void SetMsrp(string mMsrp)
        {
            _mMsrp = mMsrp;
        }

        public string GetName()
        {
            return _mName;
        }

        public void SetName(string mName)
        {
            _mName = mName;
        }

        public bool GetInstalled()
        {
            return _mInstalled;
        }

        public void SetInstalled(bool mInstalled)
        {
            _mInstalled = mInstalled;
        }

        public bool GetStandard()
        {
            return _mStandard;
        }

        public void SetStandard(bool mStandard)
        {
            _mStandard = mStandard;
        }
    }
}