namespace Gallery3WinForm
{
    sealed partial class frmPainting : Gallery3WinForm.frmWork
    {        
        private frmPainting()
        {
            InitializeComponent();
        }

        public static readonly frmPainting Instance = new frmPainting();

        public static void Run(clsAllWork prPainting)
        {
            Instance.SetDetails(prPainting);
        }

        protected override void updateForm()
        {
            base.updateForm();
            //clsPainting lcWork = (clsPainting)_Work;
            txtWidth.Text = _Work.Width.ToString();
            txtHeight.Text = _Work.Height.ToString();
            txtType.Text = _Work.Type;
        }

        protected override void pushData()
        {
            base.pushData();
            //clsPainting lcWork = (clsPainting)_Work;
            _Work.Width = float.Parse(txtWidth.Text);
            _Work.Height = float.Parse(txtHeight.Text);
            _Work.Type = txtType.Text;
        }

    }
}

