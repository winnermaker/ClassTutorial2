namespace Gallery3WinForm
{
    sealed partial class frmPhotograph : Gallery3WinForm.frmWork
    {       
        private frmPhotograph()
        {
            InitializeComponent();
        }

        public static readonly frmPhotograph Instance = new frmPhotograph();

        public static void Run(clsAllWork prPhotograph)
        {
            Instance.SetDetails(prPhotograph);
        }

        protected override void updateForm()
        {
            base.updateForm();
            //clsPhotograph lcWork = (clsPhotograph)this._Work;
            txtWidth.Text = _Work.Width.ToString();
            txtHeight.Text = _Work.Height.ToString();
            txtType.Text = _Work.Type;
        }

        protected override void pushData()
        {
            base.pushData();
            //clsPhotograph lcWork = (clsPhotograph)_Work;
            _Work.Width = float.Parse(txtWidth.Text);
            _Work.Height = float.Parse(txtHeight.Text);
            _Work.Type = txtType.Text;
        }
    }
}

