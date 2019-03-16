namespace Version_2_C
{
    sealed partial class frmPainting : Version_2_C.frmWork
    {        
        private frmPainting()
        {
            InitializeComponent();
        }

        public static readonly frmPainting Instance = new frmPainting();

        public static void Run(clsPainting prPainting)
        {
            Instance.SetDetails(prPainting);
        }

        protected override void updateForm()
        {
            base.updateForm();
            clsPainting lcWork = (clsPainting)_Work;
            txtWidth.Text = lcWork.Width.ToString();
            txtHeight.Text = lcWork.Height.ToString();
            txtType.Text = lcWork.Type;
        }

        protected override void pushData()
        {
            base.pushData();
            clsPainting lcWork = (clsPainting)_Work;
            lcWork.Width = float.Parse(txtWidth.Text);
            lcWork.Height = float.Parse(txtHeight.Text);
            lcWork.Type = txtType.Text;
        }

    }
}

