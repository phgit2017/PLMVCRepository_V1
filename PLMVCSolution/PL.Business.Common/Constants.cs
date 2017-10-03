using System;

namespace PL.Business.Common
{
    public static class Constants
    {

        public const string ProductImageContentDir = "Content/uploads/productimages";
        public const string ProductExcelTemplateDir = "Content/excel/templates";
        public const string ProductBatchExcelUploads = "Content/uploads/batchinventoryuploads";
        public const string ProductImageContentDirRetrieve = @"Content\uploads\productimages";

        public const string BranchName = "BranchName";
        public const string DateValue = "DateValue";
        public const string TotalAmount = "TotalAmount";
        public const string DefaultProductImageContent = "default.jpg";
        public const int ProfilePictureDimensionWidth = 187;
        public const int ProfilePictureDimensionHeight= 175;
        public const string ReceiptTemplate = "Receipt";
        public const string ReportTemplate = "ExtremeLotusReportTemplate";
        public const string ReportPOTemplate = "PurchaseOrderExcelTemplate";
        public const string ReportSOTemplate = "SalesOrderExcelTemplate";
        public const string BatchUploadInventoryTemplate = "BatchProduct";
        public const string POSOFormat = "{0}{1:D4}";
        public const int UserTypeUserId = 2;
        public const int UserTypeAdminId = 1;
        public const string ProductCode = "ProductCode";
        public const string ProductName = "ProductName";
        public const string ProductExtension = "ProductExtension";
        public const string OldQuantity = "OldQuantity";
        public const string NewQuantity = "NewQuantity";
        public const string ReportPerCategory = "ReportPerCategory";
        public const string ReportPerPurchaseOrder = "ReportPerPurchaseOrder";
        public const string ReportPerSalesOrder = "ReportPerSalesOrder";
        public const string ReportSalesReport = "ReportSalesReport";
        public const string UploadRemarks = "UploadRemarks";
        public const string InventoryReport = "InventoryReport";
        public const string InventoryList = "InventoryList";
        public const string SalesTemplate = "0000000000";
    }

    public static class Globals
    {
        public const string Active = "Active";
        public const string Client = "Client";
        public const string Customer = "Customer";
        public const string ContentTypeTextPlain = "text/plain";
        public const int FemaleConstantId = 1;
        public const int MaleConstantId = 0;
        public const string Database = "Database";
        public const string DefaultCurrency = "Php";
        public const string DefaultHomeNumMaskedText = "(000) 000 0000";
        public const string DefaultMobileNumMaskedText = "+63 \\900 000 0000";
        public const string DefaultPagIbigMaskedText = "0000-0000-0000";
        public const string DefaultPassportMaskedText = "LL0000000";
        public const string DefaultPhilHealthMaskedText = "00-000000000-0";
        public const string DefaultRecordDateFormat = "MM/dd/yyyy";
        public const string DefaultRecordDateTimeFormat = "MM/dd/yyyy hh:mm:ss tt";
        public const string DefaultRecordShortCompleteDateFormat = "MMM dd, yyyy";
        public const string DefaultRecordShortCompleteDateTimeFormat = "MMM dd, yyyy hh:mm:ss tt";
        public const string DefaultRecordNoDayDateFormat = "MMM yyyy";
        public const string DefaultSSSMaskedText = "00-0000000-0";
        public const string DefaultTINMaskedText = "000-000-000-000";
        public const string Failed = "Failed";
        public const string Female = "Female";
        public const string Male = "Male";
        public const string Start = "Start";
        public const string Success = "Success";
        public const string NoneMobileNumber = "000 000 0000";
        public const string NotSpecified = "Not Specified";
        public const string NotSpecifiedList = "Not Specified,N/A,NA";
        public const string NULL = "NULL";
        public const string Uploaded = "Uploaded";
        public const string Uploading = "Uploading";
        public const string Pending = "Pending";
        public const string Result = "Result";
    }

    public static class Applications
    {
        public const string IOBalance = "IOBalance";
        public const string iobalance = "iobalance";
    }

    public static class Messages
    {
        public const string AccountLockedOut = "Unable to login. Your account is currently locked out. Please try again after {0} minute(s).";
        public const string AccountUserIncorrect = "The user name or password provided is incorrect.";
        public const string AccountUserIsInactive = "The user you are logging in is not active";
        public const string BatchNoEmpty = "Batch number is empty, kindly retry uploading your file (This is a SYSTEM ISSUE)";
        public const string BatchUploadErrorAllRecords = "All record contains error, Please check the file and re-upload again";
        public const string BulkInsertError = "There is a problem in INSERTING Of Bulk Products";
        public const string CaptchaError = "Please check the above captcha.";
        public const string ChangePasswordFailed = "Unable to change password. Please correct the errors.";
        public const string ChangeSecurityParametersFailed = "Unable to change security question and answer. Please correct the erors.";
        public const string ChangeSuccess = "{0} successfully changed.";
        public const string DeleteSuccessRecord = "{0} successfully deleted.";
        public const string DeleteSuccess = "successfully deleted record";
        public const string DeleteFailed = "There's an error deleting the {0}.";
        public const string DuplicateUserInBranch = "Please use a different username in particular Branch/User Type";
        public const string DuplicateItem = "System detect that you have duplicate {0}";
        public const string DuplicateItemInBranch = "System detect that you have duplicate {0} in particular branch";
        public const string ErrorOccuredDuringProcessing = "An error occured during the process. Please check the details, refresh the page, and try again.";
        public const string ErrorOccuredDuringProcessingThis = "An error occured during the process {{0}}. Please check the details, refresh the page, and try again.";
        public const string ErrorOccuredDuringBatchProcessing = "An error occured during the batch process. Please check the details, refresh the page, and try again.";
        public const string ExcelUploadedEmpty = "Excel uploaded is empty";
        public const string ExcelUploadNumberOfColumns = "Please download the template, this template may not be the same";
        public const string FieldRequired = "{0} is required.";
        public const string FileExceedLimit = "File must not exceed {0}.";
        public const string FileNameTooLong = "The file name is too long. It can only be {0} characters long.";
        public const string FileTypeNotAccepted = "The file type is not accepted";
        public const string FutureDate = "{0} should not be later than today.";
        public const string GenericFailed = "An error occurred during the process in the system.";
        public const string IncorrectEntry = "{0} is incorrect";
        public const string InsertSuccess = "Sucessfully inserted record";
        public const string InsertSuccessRecord = "Successfully inserted {0}!";
        public const string InsertSuccessButWithError = "Successfully inserted records but other data had an error";
        public const string InvalidAge = "{0} must be {1} years or above.";
        public const string InvalidDateEnd = "End date must be greater than Start date";
        public const string InvalidEmail = "Invalid Email Address";
        public const string InvalidFileType = "Invalid file type";
        public const string InvalidSecurityCode = "Invalid Security Code";
        public const string ManualRegistrationAccountFailed = "Update account failed. Please try again.";
        public const string MaxLengthExceeded = "Up to {0} characters only.";
        public const string NoApplicantFound = "No applicant found.";
        public const string NoProductFound = "No Product found.";
        public const string NoRowFound = "No Row Found";
        public const string NoSelectedFile = "No selected file.";
        public const string NotFound = "Not Found";
        public const string NotificationFailed = "Notification Failed";
        public const string NotificationSent = "Notification Sent";
        public const string PleaseSelectItem = "Please select {0}";
        public const string RecordExist = "{0} already exists.";
        public const string RecordNotExists = "{0} does not exists.";
        public const string RecoveryEmailNotExists = "Sorry, we are unable to locate the account that matches the email provided.";
        public const string SpaceNotAllowed = "Space are not allowed.";
        public const string Unauthorized = "Unauthorized";
        public const string UpdateError = "Unable to update {0}. Please correct the errors.";
        public const string UpdateFailed = "Update Failed. Please refresh the page and try again.";
        public const string UpdateSuccess = "successfully updated record";
        public const string UpdateSuccessRecord = "{0} successfully updated";
        public const string UploadSuccess = "{0} successfully uploaded";
    }

    public static class Modules
    {
        public const string Account = "Account";
        public const string Category = "Category";
        public const string Product = "Product";
        public const string Supplier = "Supplier";
        public const string User = "User";
        
    }
}
