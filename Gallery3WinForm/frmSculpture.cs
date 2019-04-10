namespace Gallery3WinForm
{
    sealed partial class frmSculpture : Gallery3WinForm.frmWork
    {        
        private frmSculpture()
        {
            InitializeComponent();
        }
        public static readonly frmSculpture Instance = new frmSculpture();

        public static void Run(clsAllWork prSculpture)
        {
            Instance.SetDetails(prSculpture);
        }

        protected override void updateForm()
        {
            base.updateForm();
            //clsSculpture lcWork = (clsSculpture)this._Work;
            txtWeight.Text = _Work.Weight.ToString();
            txtMaterial.Text = _Work.Material;
        }

        protected override void pushData()
        {
            base.pushData();
            //clsSculpture lcWork = (clsSculpture)_Work;
            _Work.Weight = float.Parse(txtWeight.Text);
            _Work.Material = txtMaterial.Text;
        }
    }
}

