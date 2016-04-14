using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Business_Layer;

namespace CSLAWinFormCsharp
{
      public partial class Form1 : Form
       
    {
        private PatientList _patientListClass = new PatientList();
        private BindingSource _gridBindingSource = new BindingSource();
        private BindingSource _addressGridBindingSource = new BindingSource();
        private BindingList<PatientEdit> _listOfPatients = new BindingList<PatientEdit>();
        private BindingList<AddressEdit> _addressList = new BindingList<AddressEdit>();
        private PatientEdit _currPatient = new PatientEdit();
        private AddressEdit _currAddress = new AddressEdit();

        public Form1()
        {
            InitializeComponent();
            UpdatePatientList();
            this.dataGridView1.SelectionChanged += new System.EventHandler(SelectedPatient_Changed);
            this.AddressGridView2.SelectionChanged += new System.EventHandler(Address_Changed);
        }


        #region Properties
        //private string _firstName;
        //              public string FirstName
        //{
        //    get { return _currPatient.FirstName; }
        //    set
        //    {
        //        _currPatient.FirstName = value;
        //        InvokePropertyChanged(new PropertyChangedEventArgs("FirstName"));
        //    }
        //}

        //public string LastName
        //{
        //    get { return _currPatient.LastName; }
        //    set
        //    {
        //        _currPatient.LastName = value;
        //        InvokePropertyChanged(new PropertyChangedEventArgs("LastName"));
        //    }
        //}

        //public DateTime DateOfBirth
        //{
        //    get {
        //        if (_currPatient.DateOfBirth < DateTime.MinValue)
        //        { _currPatient.DateOfBirth = DateTime.MinValue; }
        //        return _currPatient.DateOfBirth;
        //    }
        //    set
        //    {
        //        if (_currPatient.DateOfBirth < DateTime.MinValue )
        //        { _currPatient.DateOfBirth = DateTime.MinValue; }

        //        _currPatient.DateOfBirth = value;
        //        InvokePropertyChanged(new PropertyChangedEventArgs("DateOfBirth"));
        //    }
        //}

        //public string Address
        //{
        //    get {
        //        if (_currPatient.AddressList != null)
        //        { return _currPatient.AddressList[0].AddressLine1; }
        //        else
        //            return string.Empty;

        //    }
        //    set
        //    {
        //        _currPatient.AddressList[0].AddressLine1 = value;
        //        InvokePropertyChanged(new PropertyChangedEventArgs("Address"));
        //    }
        //}

        //public string Postcode
        //{
        //    get
        //    {
        //        if (_currPatient.AddressList != null)
        //        { return _currPatient.AddressList[0].PostCode; }
        //        else
        //            return string.Empty;

        //    }
        //    set
        //    {
        //        _currPatient.AddressList[0].PostCode = value;
        //        InvokePropertyChanged(new PropertyChangedEventArgs("Postcode"));
        //    }
        //}
        //        public string Phone
        //{
        //    get
        //    {
        //        if (_currPatient.AddressList != null)
        //        { return _currPatient.AddressList[0].PhoneNumber; }
        //        else
        //            return string.Empty;

        //    }
        //    set
        //    {
        //        _currPatient.AddressList[0].PhoneNumber = value;
        //        InvokePropertyChanged(new PropertyChangedEventArgs("Phone"));
        //    }
        //}

        //public string Email
        //{
        //    get
        //    {
        //        if (_currPatient.AddressList != null)
        //        { return _currPatient.AddressList[0].EmailAddress; }
        //        else
        //            return string.Empty;

        //    }
        //    set
        //    {
        //        _currPatient.AddressList[0].EmailAddress = value;
        //        InvokePropertyChanged(new PropertyChangedEventArgs("Email"));
        //    }
        //}

        #endregion

        #region Events
 
        private void SelectedPatient_Changed(object sender, EventArgs e)
        {
            DataGridViewRow currRow = dataGridView1.CurrentRow;
            Guid pxID = (Guid)(currRow.Cells[0].Value);
            _currPatient = PatientEdit.GetPatientEdit(pxID);

            AddPatientDataBindings();
            UpdateAddressList();
        }
        private void Address_Changed(object sender, EventArgs e)
        {
            DataGridViewRow currRow = AddressGridView2.CurrentRow;
            Guid addressID = (Guid)(currRow.Cells[0].Value);
            _currAddress = (from b in _currPatient.AddressList where b.Id == addressID select b).FirstOrDefault() ;

            AddAddressDataBindings();
        }
          
        #endregion

        
        #region Data bindings
        private void UpdatePatientList()
        {
            List<PatientEdit> returnList = _patientListClass.GetPatientList();
            _listOfPatients = new BindingList<PatientEdit>(returnList);

            _gridBindingSource = new BindingSource(_listOfPatients, null);
            dataGridView1.DataSource = _gridBindingSource;
                    }

        private void UpdateAddressList()
        {
            _addressList = new BindingList<AddressEdit>();
            foreach (AddressEdit adrs in _currPatient.AddressList)
                {
                _addressList.Add(new AddressEdit
                {
                    AddressLine1 = adrs.AddressLine1,
                    PostCode = adrs.PostCode,
                    PhoneNumber = adrs.PhoneNumber,
                    EmailAddress = adrs.EmailAddress
                });
            }
            
            _addressGridBindingSource = new BindingSource(_addressList, null);
            AddressGridView2.DataSource = _addressGridBindingSource;
        }

        private void AddPatientDataBindings()
        {
            ClearPatientDataBindings();

            FirstNameBox.DataBindings.Add("Text", _currPatient, "FirstName", false, DataSourceUpdateMode.OnPropertyChanged);
            LastNameBox.DataBindings.Add("Text", _currPatient, "LastName", false, DataSourceUpdateMode.OnPropertyChanged);
            DateOfBirthPicker.DataBindings.Add("Value", _currPatient, "DateOfBirth", false, DataSourceUpdateMode.OnPropertyChanged);
        }

       
        private void AddAddressDataBindings()
        {
            if (_currAddress != null)
            {
ClearAddressDataBindings();

            AddressBox.DataBindings.Add("Text", _currAddress, "AddressLine1", false, DataSourceUpdateMode.OnPropertyChanged);
            PostcodeBox.DataBindings.Add("Text", _currAddress, "PostCode", false, DataSourceUpdateMode.OnPropertyChanged);
            EmailBox.DataBindings.Add("Text", _currAddress, "EmailAddress", false, DataSourceUpdateMode.OnPropertyChanged);
            PhoneBox.DataBindings.Add("Text", _currAddress, "PhoneNumber", false, DataSourceUpdateMode.OnPropertyChanged);
            }
            
        }

        private void ClearPatientDataBindings()
        {
            FirstNameBox.DataBindings.Clear();
            LastNameBox.DataBindings.Clear();
            DateOfBirthPicker.DataBindings.Clear();
        }

        private void ClearAddressDataBindings()
        {
            AddressBox.DataBindings.Clear();
            PostcodeBox.DataBindings.Clear();
            EmailBox.DataBindings.Clear();
            PhoneBox.DataBindings.Clear();
        }
        #endregion

        #region Control actions
        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            UpdatePatientList();
        }

        private void BtnNewPx_Click(object sender, EventArgs e)
        {
            PatientEdit.NewPatientEdit();
           // BindDetailsToCurrentPatient();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ClearPatientDataBindings();

                if (FirstNameBox.Text != string.Empty &&
                    LastNameBox.Text != string.Empty &&
                              AddressBox.Text != string.Empty &&
                    PostcodeBox.Text != string.Empty &&
                    PhoneBox.Text != string.Empty &&
                    EmailBox.Text != string.Empty)
                {
                    _currPatient.ApplyEdit();

                    _currPatient = _currPatient.Save();
                }
                else
                    MessageBox.Show("Fields missing. Please complete patient and try again");
            }
            finally
            {
                AddPatientDataBindings();
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            if (FirstNameBox.Text != string.Empty ||
                LastNameBox.Text != string.Empty ||
                AddressBox.Text != string.Empty ||
                PostcodeBox.Text != string.Empty ||
                PhoneBox.Text != string.Empty ||
                EmailBox.Text != string.Empty)
            {
                DialogResult alert = MessageBox.Show("Are you sure you want to abandon progress on this patient?", "Cancel patient?", MessageBoxButtons.YesNo);
                if (alert == DialogResult.Yes)
                {
                    _currPatient = new PatientEdit();
                }

            }
            else
            { _currPatient = new PatientEdit(); };
        }

                   #endregion
    }
}
