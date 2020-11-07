//------------------------------------------------------------------------------
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
//------------------------------------------------------------------------------

namespace ServiceReference1
{
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://www.jobs.lu/webservices/", ConfigurationName="ServiceReference1.JobWSSoap")]
    public interface JobWSSoap
    {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.jobs.lu/webservices/GetEmployerStats", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(JobServiceBaseResponse))]
        System.Threading.Tasks.Task<ServiceReference1.EmployerStatsResponse> GetEmployerStatsAsync(string login, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.jobs.lu/webservices/ExpireJob", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(JobServiceBaseResponse))]
        System.Threading.Tasks.Task<ServiceReference1.ExpireJobsResponse> ExpireJobAsync(string login, string password, int jobId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.jobs.lu/webservices/PostJob", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(JobServiceBaseResponse))]
        System.Threading.Tasks.Task<ServiceReference1.CreateJobResponse> PostJobAsync(string login, string password, ServiceReference1.Job job);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.jobs.lu/webservices/RefreshJob", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(JobServiceBaseResponse))]
        System.Threading.Tasks.Task<ServiceReference1.RefreshResponse> RefreshJobAsync(string login, string password, int jobId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.jobs.lu/webservices/UpdateJob", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(JobServiceBaseResponse))]
        System.Threading.Tasks.Task<ServiceReference1.UpdateResponse> UpdateJobAsync(string login, string password, ServiceReference1.Job job);
        
        // CODEGEN: Generating message contract since the operation has multiple return values.
        [System.ServiceModel.OperationContractAttribute(Action="http://www.jobs.lu/webservices/SearchJobs", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(JobServiceBaseResponse))]
        System.Threading.Tasks.Task<ServiceReference1.SearchJobsResponse> SearchJobsAsync(ServiceReference1.SearchJobsRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.jobs.lu/webservices/GetJobDisplay", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(JobServiceBaseResponse))]
        System.Threading.Tasks.Task<ServiceReference1.JobDisplay> GetJobDisplayAsync(int jobId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.jobs.lu/webservices/AddApplication", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(JobServiceBaseResponse))]
        System.Threading.Tasks.Task<bool> AddApplicationAsync(ServiceReference1.ApplicationData application);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.jobs.lu/webservices/GetJobCategories", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(JobServiceBaseResponse))]
        System.Threading.Tasks.Task<ServiceReference1.GetJobCategoriesResponse> GetJobCategoriesAsync(ServiceReference1.GetJobCategoriesRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.jobs.lu/webservices/GetIndustries", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(JobServiceBaseResponse))]
        System.Threading.Tasks.Task<ServiceReference1.GetIndustriesResponse> GetIndustriesAsync(ServiceReference1.GetIndustriesRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.jobs.lu/webservices/GetJobRegions", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(JobServiceBaseResponse))]
        System.Threading.Tasks.Task<ServiceReference1.GetJobRegionsResponse> GetJobRegionsAsync(ServiceReference1.GetJobRegionsRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.jobs.lu/webservices/GetCountries", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(JobServiceBaseResponse))]
        System.Threading.Tasks.Task<ServiceReference1.GetCountriesResponse> GetCountriesAsync(ServiceReference1.GetCountriesRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.jobs.lu/webservices/GetAddressRegions", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(JobServiceBaseResponse))]
        System.Threading.Tasks.Task<ServiceReference1.GetAddressRegionsResponse> GetAddressRegionsAsync(ServiceReference1.GetAddressRegionsRequest request);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.jobs.lu/webservices/")]
    public partial class EmployerStatsResponse : JobServiceBaseResponse
    {
        
        private EmployerStats employerStatsField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public EmployerStats EmployerStats
        {
            get
            {
                return this.employerStatsField;
            }
            set
            {
                this.employerStatsField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.jobs.lu/webservices/")]
    public partial class EmployerStats
    {
        
        private int employerIdField;
        
        private int activeJobCountField;
        
        private int totalJobCountField;
        
        private int activeAdvertLimitField;
        
        private bool isSubscriptionEnabledField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public int EmployerId
        {
            get
            {
                return this.employerIdField;
            }
            set
            {
                this.employerIdField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public int ActiveJobCount
        {
            get
            {
                return this.activeJobCountField;
            }
            set
            {
                this.activeJobCountField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public int TotalJobCount
        {
            get
            {
                return this.totalJobCountField;
            }
            set
            {
                this.totalJobCountField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public int ActiveAdvertLimit
        {
            get
            {
                return this.activeAdvertLimitField;
            }
            set
            {
                this.activeAdvertLimitField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=4)]
        public bool IsSubscriptionEnabled
        {
            get
            {
                return this.isSubscriptionEnabledField;
            }
            set
            {
                this.isSubscriptionEnabledField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.jobs.lu/webservices/")]
    public partial class AddressRegion
    {
        
        private int idField;
        
        private string nameField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public int Id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.jobs.lu/webservices/")]
    public partial class Country
    {
        
        private int idField;
        
        private string nameField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public int Id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.jobs.lu/webservices/")]
    public partial class JobRegion
    {
        
        private int idField;
        
        private string nameField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public int Id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.jobs.lu/webservices/")]
    public partial class Industry
    {
        
        private int idField;
        
        private string nameField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public int Id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.jobs.lu/webservices/")]
    public partial class JobCategory
    {
        
        private int idField;
        
        private string nameField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public int Id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.jobs.lu/webservices/")]
    public partial class ApplicationData
    {
        
        private string coverNoteField;
        
        private string nameField;
        
        private string phoneField;
        
        private string emailField;
        
        private string answer1Field;
        
        private string answer2Field;
        
        private string answer3Field;
        
        private int jobIdField;
        
        private string cVFileNameField;
        
        private string cVFileTypeField;
        
        private byte[] cVFileDataField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string CoverNote
        {
            get
            {
                return this.coverNoteField;
            }
            set
            {
                this.coverNoteField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string Phone
        {
            get
            {
                return this.phoneField;
            }
            set
            {
                this.phoneField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public string Email
        {
            get
            {
                return this.emailField;
            }
            set
            {
                this.emailField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=4)]
        public string Answer1
        {
            get
            {
                return this.answer1Field;
            }
            set
            {
                this.answer1Field = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=5)]
        public string Answer2
        {
            get
            {
                return this.answer2Field;
            }
            set
            {
                this.answer2Field = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=6)]
        public string Answer3
        {
            get
            {
                return this.answer3Field;
            }
            set
            {
                this.answer3Field = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=7)]
        public int JobId
        {
            get
            {
                return this.jobIdField;
            }
            set
            {
                this.jobIdField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=8)]
        public string CVFileName
        {
            get
            {
                return this.cVFileNameField;
            }
            set
            {
                this.cVFileNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=9)]
        public string CVFileType
        {
            get
            {
                return this.cVFileTypeField;
            }
            set
            {
                this.cVFileTypeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary", Order=10)]
        public byte[] CVFileData
        {
            get
            {
                return this.cVFileDataField;
            }
            set
            {
                this.cVFileDataField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.jobs.lu/webservices/")]
    public partial class JobDisplay
    {
        
        private int idField;
        
        private int employerIdField;
        
        private string titleField;
        
        private string descriptionField;
        
        private string htmlDescriptionField;
        
        private string locationField;
        
        private string paymentField;
        
        private string referenceField;
        
        private System.DateTime modifiedDTField;
        
        private System.DateTime createdDTField;
        
        private string applicationURLField;
        
        private string question1Field;
        
        private string question2Field;
        
        private string question3Field;
        
        private bool isFullTimeField;
        
        private bool isPartTimeField;
        
        private bool isPermanentField;
        
        private bool isContractField;
        
        private bool hasLogoField;
        
        private string homepageUrlField;
        
        private string companyField;
        
        private string addressField;
        
        private string phoneField;
        
        private string faxField;
        
        private string emailField;
        
        private string contactField;
        
        private string[] categoryNamesField;
        
        private string[] regionNamesField;
        
        private int[] categoryIdsField;
        
        private int[] regionIdsField;
        
        private bool isContactHiddenField;
        
        private bool isCompanyNameHiddenField;
        
        private bool isEmailHiddenField;
        
        private bool isPhoneHiddenField;
        
        private bool isFaxHiddenField;
        
        private bool isAddressHiddenField;
        
        private bool isHomePageHiddenField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public int Id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public int EmployerId
        {
            get
            {
                return this.employerIdField;
            }
            set
            {
                this.employerIdField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string Title
        {
            get
            {
                return this.titleField;
            }
            set
            {
                this.titleField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public string Description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=4)]
        public string HtmlDescription
        {
            get
            {
                return this.htmlDescriptionField;
            }
            set
            {
                this.htmlDescriptionField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=5)]
        public string Location
        {
            get
            {
                return this.locationField;
            }
            set
            {
                this.locationField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=6)]
        public string Payment
        {
            get
            {
                return this.paymentField;
            }
            set
            {
                this.paymentField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=7)]
        public string Reference
        {
            get
            {
                return this.referenceField;
            }
            set
            {
                this.referenceField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=8)]
        public System.DateTime ModifiedDT
        {
            get
            {
                return this.modifiedDTField;
            }
            set
            {
                this.modifiedDTField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=9)]
        public System.DateTime CreatedDT
        {
            get
            {
                return this.createdDTField;
            }
            set
            {
                this.createdDTField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=10)]
        public string ApplicationURL
        {
            get
            {
                return this.applicationURLField;
            }
            set
            {
                this.applicationURLField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=11)]
        public string Question1
        {
            get
            {
                return this.question1Field;
            }
            set
            {
                this.question1Field = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=12)]
        public string Question2
        {
            get
            {
                return this.question2Field;
            }
            set
            {
                this.question2Field = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=13)]
        public string Question3
        {
            get
            {
                return this.question3Field;
            }
            set
            {
                this.question3Field = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=14)]
        public bool IsFullTime
        {
            get
            {
                return this.isFullTimeField;
            }
            set
            {
                this.isFullTimeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=15)]
        public bool IsPartTime
        {
            get
            {
                return this.isPartTimeField;
            }
            set
            {
                this.isPartTimeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=16)]
        public bool IsPermanent
        {
            get
            {
                return this.isPermanentField;
            }
            set
            {
                this.isPermanentField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=17)]
        public bool IsContract
        {
            get
            {
                return this.isContractField;
            }
            set
            {
                this.isContractField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=18)]
        public bool HasLogo
        {
            get
            {
                return this.hasLogoField;
            }
            set
            {
                this.hasLogoField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=19)]
        public string HomepageUrl
        {
            get
            {
                return this.homepageUrlField;
            }
            set
            {
                this.homepageUrlField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=20)]
        public string Company
        {
            get
            {
                return this.companyField;
            }
            set
            {
                this.companyField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=21)]
        public string Address
        {
            get
            {
                return this.addressField;
            }
            set
            {
                this.addressField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=22)]
        public string Phone
        {
            get
            {
                return this.phoneField;
            }
            set
            {
                this.phoneField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=23)]
        public string Fax
        {
            get
            {
                return this.faxField;
            }
            set
            {
                this.faxField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=24)]
        public string Email
        {
            get
            {
                return this.emailField;
            }
            set
            {
                this.emailField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=25)]
        public string Contact
        {
            get
            {
                return this.contactField;
            }
            set
            {
                this.contactField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Order=26)]
        public string[] CategoryNames
        {
            get
            {
                return this.categoryNamesField;
            }
            set
            {
                this.categoryNamesField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Order=27)]
        public string[] RegionNames
        {
            get
            {
                return this.regionNamesField;
            }
            set
            {
                this.regionNamesField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Order=28)]
        public int[] CategoryIds
        {
            get
            {
                return this.categoryIdsField;
            }
            set
            {
                this.categoryIdsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Order=29)]
        public int[] RegionIds
        {
            get
            {
                return this.regionIdsField;
            }
            set
            {
                this.regionIdsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=30)]
        public bool IsContactHidden
        {
            get
            {
                return this.isContactHiddenField;
            }
            set
            {
                this.isContactHiddenField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=31)]
        public bool IsCompanyNameHidden
        {
            get
            {
                return this.isCompanyNameHiddenField;
            }
            set
            {
                this.isCompanyNameHiddenField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=32)]
        public bool IsEmailHidden
        {
            get
            {
                return this.isEmailHiddenField;
            }
            set
            {
                this.isEmailHiddenField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=33)]
        public bool IsPhoneHidden
        {
            get
            {
                return this.isPhoneHiddenField;
            }
            set
            {
                this.isPhoneHiddenField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=34)]
        public bool IsFaxHidden
        {
            get
            {
                return this.isFaxHiddenField;
            }
            set
            {
                this.isFaxHiddenField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=35)]
        public bool IsAddressHidden
        {
            get
            {
                return this.isAddressHiddenField;
            }
            set
            {
                this.isAddressHiddenField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=36)]
        public bool IsHomePageHidden
        {
            get
            {
                return this.isHomePageHiddenField;
            }
            set
            {
                this.isHomePageHiddenField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.jobs.lu/webservices/")]
    public partial class JobListItem
    {
        
        private int idField;
        
        private System.DateTime createdDTField;
        
        private string titleField;
        
        private string companyField;
        
        private string locationField;
        
        private int employerIdField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public int Id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public System.DateTime CreatedDT
        {
            get
            {
                return this.createdDTField;
            }
            set
            {
                this.createdDTField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string Title
        {
            get
            {
                return this.titleField;
            }
            set
            {
                this.titleField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public string Company
        {
            get
            {
                return this.companyField;
            }
            set
            {
                this.companyField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=4)]
        public string Location
        {
            get
            {
                return this.locationField;
            }
            set
            {
                this.locationField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=5)]
        public int EmployerId
        {
            get
            {
                return this.employerIdField;
            }
            set
            {
                this.employerIdField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.jobs.lu/webservices/")]
    public partial class Job
    {
        
        private int idField;
        
        private string titleField;
        
        private string paymentField;
        
        private string locationField;
        
        private string descriptionField;
        
        private string htmlDescriptionField;
        
        private bool isFullTimeField;
        
        private bool isPartTimeField;
        
        private string question1Field;
        
        private string question2Field;
        
        private string question3Field;
        
        private string applicationURLField;
        
        private string[] contactEmailsField;
        
        private System.DateTime expiryDTField;
        
        private System.DateTime createdDTField;
        
        private System.DateTime modifiedDTField;
        
        private string referenceField;
        
        private int[] categoryIdsField;
        
        private int[] regionIdsField;
        
        private int[] contractIdsField;
        
        private int slotTypeField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public int Id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string Title
        {
            get
            {
                return this.titleField;
            }
            set
            {
                this.titleField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string Payment
        {
            get
            {
                return this.paymentField;
            }
            set
            {
                this.paymentField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public string Location
        {
            get
            {
                return this.locationField;
            }
            set
            {
                this.locationField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=4)]
        public string Description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=5)]
        public string HtmlDescription
        {
            get
            {
                return this.htmlDescriptionField;
            }
            set
            {
                this.htmlDescriptionField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=6)]
        public bool IsFullTime
        {
            get
            {
                return this.isFullTimeField;
            }
            set
            {
                this.isFullTimeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=7)]
        public bool IsPartTime
        {
            get
            {
                return this.isPartTimeField;
            }
            set
            {
                this.isPartTimeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=8)]
        public string Question1
        {
            get
            {
                return this.question1Field;
            }
            set
            {
                this.question1Field = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=9)]
        public string Question2
        {
            get
            {
                return this.question2Field;
            }
            set
            {
                this.question2Field = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=10)]
        public string Question3
        {
            get
            {
                return this.question3Field;
            }
            set
            {
                this.question3Field = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=11)]
        public string ApplicationURL
        {
            get
            {
                return this.applicationURLField;
            }
            set
            {
                this.applicationURLField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Order=12)]
        public string[] ContactEmails
        {
            get
            {
                return this.contactEmailsField;
            }
            set
            {
                this.contactEmailsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=13)]
        public System.DateTime ExpiryDT
        {
            get
            {
                return this.expiryDTField;
            }
            set
            {
                this.expiryDTField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=14)]
        public System.DateTime CreatedDT
        {
            get
            {
                return this.createdDTField;
            }
            set
            {
                this.createdDTField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=15)]
        public System.DateTime ModifiedDT
        {
            get
            {
                return this.modifiedDTField;
            }
            set
            {
                this.modifiedDTField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=16)]
        public string Reference
        {
            get
            {
                return this.referenceField;
            }
            set
            {
                this.referenceField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Order=17)]
        public int[] CategoryIds
        {
            get
            {
                return this.categoryIdsField;
            }
            set
            {
                this.categoryIdsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Order=18)]
        public int[] RegionIds
        {
            get
            {
                return this.regionIdsField;
            }
            set
            {
                this.regionIdsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Order=19)]
        public int[] ContractIds
        {
            get
            {
                return this.contractIdsField;
            }
            set
            {
                this.contractIdsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=20)]
        public int SlotType
        {
            get
            {
                return this.slotTypeField;
            }
            set
            {
                this.slotTypeField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(SlotBalanceAgencySlots))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(SlotBalanceAgencyCredits))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(SlotBalanceCorporate))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(SlotBalanceJobsLu))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.jobs.lu/webservices/")]
    public abstract partial class SlotBalance
    {
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.jobs.lu/webservices/")]
    public partial class SlotBalanceAgencySlots : SlotBalance
    {
        
        private int totalSlotsField;
        
        private int slotsInUseField;
        
        private int freeSlotsField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public int TotalSlots
        {
            get
            {
                return this.totalSlotsField;
            }
            set
            {
                this.totalSlotsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public int SlotsInUse
        {
            get
            {
                return this.slotsInUseField;
            }
            set
            {
                this.slotsInUseField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public int FreeSlots
        {
            get
            {
                return this.freeSlotsField;
            }
            set
            {
                this.freeSlotsField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.jobs.lu/webservices/")]
    public partial class SlotBalanceAgencyCredits : SlotBalance
    {
        
        private int totalCreditsField;
        
        private int creditsRemainingField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public int TotalCredits
        {
            get
            {
                return this.totalCreditsField;
            }
            set
            {
                this.totalCreditsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public int CreditsRemaining
        {
            get
            {
                return this.creditsRemainingField;
            }
            set
            {
                this.creditsRemainingField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.jobs.lu/webservices/")]
    public partial class SlotBalanceCorporate : SlotBalance
    {
        
        private int standardSlotsFreeField;
        
        private int standardSlotsInUseField;
        
        private int professionalSlotsFreeField;
        
        private int professionalSlotsInUseField;
        
        private int premiumSlotsFreeField;
        
        private int premiumSlotsInUseField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public int StandardSlotsFree
        {
            get
            {
                return this.standardSlotsFreeField;
            }
            set
            {
                this.standardSlotsFreeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public int StandardSlotsInUse
        {
            get
            {
                return this.standardSlotsInUseField;
            }
            set
            {
                this.standardSlotsInUseField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public int ProfessionalSlotsFree
        {
            get
            {
                return this.professionalSlotsFreeField;
            }
            set
            {
                this.professionalSlotsFreeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public int ProfessionalSlotsInUse
        {
            get
            {
                return this.professionalSlotsInUseField;
            }
            set
            {
                this.professionalSlotsInUseField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=4)]
        public int PremiumSlotsFree
        {
            get
            {
                return this.premiumSlotsFreeField;
            }
            set
            {
                this.premiumSlotsFreeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=5)]
        public int PremiumSlotsInUse
        {
            get
            {
                return this.premiumSlotsInUseField;
            }
            set
            {
                this.premiumSlotsInUseField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.jobs.lu/webservices/")]
    public partial class SlotBalanceJobsLu : SlotBalance
    {
        
        private int slotsFreeField;
        
        private int slotsInUseField;
        
        private int totalCreditsField;
        
        private int creditsRemainingField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public int SlotsFree
        {
            get
            {
                return this.slotsFreeField;
            }
            set
            {
                this.slotsFreeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public int SlotsInUse
        {
            get
            {
                return this.slotsInUseField;
            }
            set
            {
                this.slotsInUseField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public int TotalCredits
        {
            get
            {
                return this.totalCreditsField;
            }
            set
            {
                this.totalCreditsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public int CreditsRemaining
        {
            get
            {
                return this.creditsRemainingField;
            }
            set
            {
                this.creditsRemainingField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(UpdateResponse))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(RefreshResponse))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(CreateJobResponse))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(ExpireJobsResponse))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(EmployerStatsResponse))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.jobs.lu/webservices/")]
    public abstract partial class JobServiceBaseResponse
    {
        
        private bool successField;
        
        private string messageField;
        
        private SlotBalance balanceField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public bool Success
        {
            get
            {
                return this.successField;
            }
            set
            {
                this.successField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string Message
        {
            get
            {
                return this.messageField;
            }
            set
            {
                this.messageField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public SlotBalance Balance
        {
            get
            {
                return this.balanceField;
            }
            set
            {
                this.balanceField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.jobs.lu/webservices/")]
    public partial class UpdateResponse : JobServiceBaseResponse
    {
        
        private int updateCountField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public int UpdateCount
        {
            get
            {
                return this.updateCountField;
            }
            set
            {
                this.updateCountField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.jobs.lu/webservices/")]
    public partial class RefreshResponse : JobServiceBaseResponse
    {
        
        private int refreshCountField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public int RefreshCount
        {
            get
            {
                return this.refreshCountField;
            }
            set
            {
                this.refreshCountField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.jobs.lu/webservices/")]
    public partial class CreateJobResponse : JobServiceBaseResponse
    {
        
        private int jobIdField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public int JobId
        {
            get
            {
                return this.jobIdField;
            }
            set
            {
                this.jobIdField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.jobs.lu/webservices/")]
    public partial class ExpireJobsResponse : JobServiceBaseResponse
    {
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.jobs.lu/webservices/")]
    public enum JobDuration
    {
        
        /// <remarks/>
        Any,
        
        /// <remarks/>
        Permanent,
        
        /// <remarks/>
        Contract,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.jobs.lu/webservices/")]
    public enum JobHours
    {
        
        /// <remarks/>
        Any,
        
        /// <remarks/>
        FullTime,
        
        /// <remarks/>
        PartTime,
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="SearchJobs", WrapperNamespace="http://www.jobs.lu/webservices/", IsWrapped=true)]
    public partial class SearchJobsRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.jobs.lu/webservices/", Order=0)]
        public int[] employerIds;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.jobs.lu/webservices/", Order=1)]
        public int[] jobCategoryIds;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.jobs.lu/webservices/", Order=2)]
        public int[] regionIds;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.jobs.lu/webservices/", Order=3)]
        public bool excludeAgencies;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.jobs.lu/webservices/", Order=4)]
        public int[] industryIds;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.jobs.lu/webservices/", Order=5)]
        public ServiceReference1.JobDuration jobType;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.jobs.lu/webservices/", Order=6)]
        public ServiceReference1.JobHours jobHours;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.jobs.lu/webservices/", Order=7)]
        public string keywords;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.jobs.lu/webservices/", Order=8)]
        public System.DateTime fromDate;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.jobs.lu/webservices/", Order=9)]
        public System.DateTime toDate;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.jobs.lu/webservices/", Order=10)]
        public int startRecord;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.jobs.lu/webservices/", Order=11)]
        public int pageSize;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.jobs.lu/webservices/", Order=12)]
        public string key;
        
        public SearchJobsRequest()
        {
        }
        
        public SearchJobsRequest(int[] employerIds, int[] jobCategoryIds, int[] regionIds, bool excludeAgencies, int[] industryIds, ServiceReference1.JobDuration jobType, ServiceReference1.JobHours jobHours, string keywords, System.DateTime fromDate, System.DateTime toDate, int startRecord, int pageSize, string key)
        {
            this.employerIds = employerIds;
            this.jobCategoryIds = jobCategoryIds;
            this.regionIds = regionIds;
            this.excludeAgencies = excludeAgencies;
            this.industryIds = industryIds;
            this.jobType = jobType;
            this.jobHours = jobHours;
            this.keywords = keywords;
            this.fromDate = fromDate;
            this.toDate = toDate;
            this.startRecord = startRecord;
            this.pageSize = pageSize;
            this.key = key;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="SearchJobsResponse", WrapperNamespace="http://www.jobs.lu/webservices/", IsWrapped=true)]
    public partial class SearchJobsResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.jobs.lu/webservices/", Order=0)]
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable=false)]
        public ServiceReference1.JobListItem[] SearchJobsResult;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.jobs.lu/webservices/", Order=1)]
        public int recordCount;
        
        public SearchJobsResponse()
        {
        }
        
        public SearchJobsResponse(ServiceReference1.JobListItem[] SearchJobsResult, int recordCount)
        {
            this.SearchJobsResult = SearchJobsResult;
            this.recordCount = recordCount;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="GetJobCategories", WrapperNamespace="http://www.jobs.lu/webservices/", IsWrapped=true)]
    public partial class GetJobCategoriesRequest
    {
        
        public GetJobCategoriesRequest()
        {
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="GetJobCategoriesResponse", WrapperNamespace="http://www.jobs.lu/webservices/", IsWrapped=true)]
    public partial class GetJobCategoriesResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.jobs.lu/webservices/", Order=0)]
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable=false)]
        public ServiceReference1.JobCategory[] GetJobCategoriesResult;
        
        public GetJobCategoriesResponse()
        {
        }
        
        public GetJobCategoriesResponse(ServiceReference1.JobCategory[] GetJobCategoriesResult)
        {
            this.GetJobCategoriesResult = GetJobCategoriesResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="GetIndustries", WrapperNamespace="http://www.jobs.lu/webservices/", IsWrapped=true)]
    public partial class GetIndustriesRequest
    {
        
        public GetIndustriesRequest()
        {
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="GetIndustriesResponse", WrapperNamespace="http://www.jobs.lu/webservices/", IsWrapped=true)]
    public partial class GetIndustriesResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.jobs.lu/webservices/", Order=0)]
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable=false)]
        public ServiceReference1.Industry[] GetIndustriesResult;
        
        public GetIndustriesResponse()
        {
        }
        
        public GetIndustriesResponse(ServiceReference1.Industry[] GetIndustriesResult)
        {
            this.GetIndustriesResult = GetIndustriesResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="GetJobRegions", WrapperNamespace="http://www.jobs.lu/webservices/", IsWrapped=true)]
    public partial class GetJobRegionsRequest
    {
        
        public GetJobRegionsRequest()
        {
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="GetJobRegionsResponse", WrapperNamespace="http://www.jobs.lu/webservices/", IsWrapped=true)]
    public partial class GetJobRegionsResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.jobs.lu/webservices/", Order=0)]
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable=false)]
        public ServiceReference1.JobRegion[] GetJobRegionsResult;
        
        public GetJobRegionsResponse()
        {
        }
        
        public GetJobRegionsResponse(ServiceReference1.JobRegion[] GetJobRegionsResult)
        {
            this.GetJobRegionsResult = GetJobRegionsResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="GetCountries", WrapperNamespace="http://www.jobs.lu/webservices/", IsWrapped=true)]
    public partial class GetCountriesRequest
    {
        
        public GetCountriesRequest()
        {
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="GetCountriesResponse", WrapperNamespace="http://www.jobs.lu/webservices/", IsWrapped=true)]
    public partial class GetCountriesResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.jobs.lu/webservices/", Order=0)]
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable=false)]
        public ServiceReference1.Country[] GetCountriesResult;
        
        public GetCountriesResponse()
        {
        }
        
        public GetCountriesResponse(ServiceReference1.Country[] GetCountriesResult)
        {
            this.GetCountriesResult = GetCountriesResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="GetAddressRegions", WrapperNamespace="http://www.jobs.lu/webservices/", IsWrapped=true)]
    public partial class GetAddressRegionsRequest
    {
        
        public GetAddressRegionsRequest()
        {
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="GetAddressRegionsResponse", WrapperNamespace="http://www.jobs.lu/webservices/", IsWrapped=true)]
    public partial class GetAddressRegionsResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.jobs.lu/webservices/", Order=0)]
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable=false)]
        public ServiceReference1.AddressRegion[] GetAddressRegionsResult;
        
        public GetAddressRegionsResponse()
        {
        }
        
        public GetAddressRegionsResponse(ServiceReference1.AddressRegion[] GetAddressRegionsResult)
        {
            this.GetAddressRegionsResult = GetAddressRegionsResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    public interface JobWSSoapChannel : ServiceReference1.JobWSSoap, System.ServiceModel.IClientChannel
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    public partial class JobWSSoapClient : System.ServiceModel.ClientBase<ServiceReference1.JobWSSoap>, ServiceReference1.JobWSSoap
    {
        
        /// <summary>
        /// Implement this partial method to configure the service endpoint.
        /// </summary>
        /// <param name="serviceEndpoint">The endpoint to configure</param>
        /// <param name="clientCredentials">The client credentials</param>
        static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);
        
        public JobWSSoapClient(EndpointConfiguration endpointConfiguration) : 
                base(JobWSSoapClient.GetBindingForEndpoint(endpointConfiguration), JobWSSoapClient.GetEndpointAddress(endpointConfiguration))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public JobWSSoapClient(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(JobWSSoapClient.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public JobWSSoapClient(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(JobWSSoapClient.GetBindingForEndpoint(endpointConfiguration), remoteAddress)
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public JobWSSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress)
        {
        }
        
        public System.Threading.Tasks.Task<ServiceReference1.EmployerStatsResponse> GetEmployerStatsAsync(string login, string password)
        {
            return base.Channel.GetEmployerStatsAsync(login, password);
        }
        
        public System.Threading.Tasks.Task<ServiceReference1.ExpireJobsResponse> ExpireJobAsync(string login, string password, int jobId)
        {
            return base.Channel.ExpireJobAsync(login, password, jobId);
        }
        
        public System.Threading.Tasks.Task<ServiceReference1.CreateJobResponse> PostJobAsync(string login, string password, ServiceReference1.Job job)
        {
            return base.Channel.PostJobAsync(login, password, job);
        }
        
        public System.Threading.Tasks.Task<ServiceReference1.RefreshResponse> RefreshJobAsync(string login, string password, int jobId)
        {
            return base.Channel.RefreshJobAsync(login, password, jobId);
        }
        
        public System.Threading.Tasks.Task<ServiceReference1.UpdateResponse> UpdateJobAsync(string login, string password, ServiceReference1.Job job)
        {
            return base.Channel.UpdateJobAsync(login, password, job);
        }
        
        public System.Threading.Tasks.Task<ServiceReference1.SearchJobsResponse> SearchJobsAsync(ServiceReference1.SearchJobsRequest request)
        {
            return base.Channel.SearchJobsAsync(request);
        }
        
        public System.Threading.Tasks.Task<ServiceReference1.JobDisplay> GetJobDisplayAsync(int jobId)
        {
            return base.Channel.GetJobDisplayAsync(jobId);
        }
        
        public System.Threading.Tasks.Task<bool> AddApplicationAsync(ServiceReference1.ApplicationData application)
        {
            return base.Channel.AddApplicationAsync(application);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ServiceReference1.GetJobCategoriesResponse> ServiceReference1.JobWSSoap.GetJobCategoriesAsync(ServiceReference1.GetJobCategoriesRequest request)
        {
            return base.Channel.GetJobCategoriesAsync(request);
        }
        
        public System.Threading.Tasks.Task<ServiceReference1.GetJobCategoriesResponse> GetJobCategoriesAsync()
        {
            ServiceReference1.GetJobCategoriesRequest inValue = new ServiceReference1.GetJobCategoriesRequest();
            return ((ServiceReference1.JobWSSoap)(this)).GetJobCategoriesAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ServiceReference1.GetIndustriesResponse> ServiceReference1.JobWSSoap.GetIndustriesAsync(ServiceReference1.GetIndustriesRequest request)
        {
            return base.Channel.GetIndustriesAsync(request);
        }
        
        public System.Threading.Tasks.Task<ServiceReference1.GetIndustriesResponse> GetIndustriesAsync()
        {
            ServiceReference1.GetIndustriesRequest inValue = new ServiceReference1.GetIndustriesRequest();
            return ((ServiceReference1.JobWSSoap)(this)).GetIndustriesAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ServiceReference1.GetJobRegionsResponse> ServiceReference1.JobWSSoap.GetJobRegionsAsync(ServiceReference1.GetJobRegionsRequest request)
        {
            return base.Channel.GetJobRegionsAsync(request);
        }
        
        public System.Threading.Tasks.Task<ServiceReference1.GetJobRegionsResponse> GetJobRegionsAsync()
        {
            ServiceReference1.GetJobRegionsRequest inValue = new ServiceReference1.GetJobRegionsRequest();
            return ((ServiceReference1.JobWSSoap)(this)).GetJobRegionsAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ServiceReference1.GetCountriesResponse> ServiceReference1.JobWSSoap.GetCountriesAsync(ServiceReference1.GetCountriesRequest request)
        {
            return base.Channel.GetCountriesAsync(request);
        }
        
        public System.Threading.Tasks.Task<ServiceReference1.GetCountriesResponse> GetCountriesAsync()
        {
            ServiceReference1.GetCountriesRequest inValue = new ServiceReference1.GetCountriesRequest();
            return ((ServiceReference1.JobWSSoap)(this)).GetCountriesAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ServiceReference1.GetAddressRegionsResponse> ServiceReference1.JobWSSoap.GetAddressRegionsAsync(ServiceReference1.GetAddressRegionsRequest request)
        {
            return base.Channel.GetAddressRegionsAsync(request);
        }
        
        public System.Threading.Tasks.Task<ServiceReference1.GetAddressRegionsResponse> GetAddressRegionsAsync()
        {
            ServiceReference1.GetAddressRegionsRequest inValue = new ServiceReference1.GetAddressRegionsRequest();
            return ((ServiceReference1.JobWSSoap)(this)).GetAddressRegionsAsync(inValue);
        }
        
        public virtual System.Threading.Tasks.Task OpenAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }
        
        public virtual System.Threading.Tasks.Task CloseAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginClose(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndClose));
        }
        
        private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.JobWSSoap))
            {
                System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                result.AllowCookies = true;
                result.Security.Mode = System.ServiceModel.BasicHttpSecurityMode.Transport;
                return result;
            }
            if ((endpointConfiguration == EndpointConfiguration.JobWSSoap12))
            {
                System.ServiceModel.Channels.CustomBinding result = new System.ServiceModel.Channels.CustomBinding();
                System.ServiceModel.Channels.TextMessageEncodingBindingElement textBindingElement = new System.ServiceModel.Channels.TextMessageEncodingBindingElement();
                textBindingElement.MessageVersion = System.ServiceModel.Channels.MessageVersion.CreateVersion(System.ServiceModel.EnvelopeVersion.Soap12, System.ServiceModel.Channels.AddressingVersion.None);
                result.Elements.Add(textBindingElement);
                System.ServiceModel.Channels.HttpsTransportBindingElement httpsBindingElement = new System.ServiceModel.Channels.HttpsTransportBindingElement();
                httpsBindingElement.AllowCookies = true;
                httpsBindingElement.MaxBufferSize = int.MaxValue;
                httpsBindingElement.MaxReceivedMessageSize = int.MaxValue;
                result.Elements.Add(httpsBindingElement);
                return result;
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.JobWSSoap))
            {
                return new System.ServiceModel.EndpointAddress("https://en.employers.jobs.lu/JobWS.asmx");
            }
            if ((endpointConfiguration == EndpointConfiguration.JobWSSoap12))
            {
                return new System.ServiceModel.EndpointAddress("https://en.employers.jobs.lu/JobWS.asmx");
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        public enum EndpointConfiguration
        {
            
            JobWSSoap,
            
            JobWSSoap12,
        }
    }
}
