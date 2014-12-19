CREATE TABLE personalInformation(
personalInfoId BIGINT IDENTITY(1,1) PRIMARY KEY,
accountId BIGINT NOT NULL,
firstName VARCHAR(100) ,
lastName VARCHAR(100) ,
phoneNumber VARCHAR(15),
emailAddress VARCHAR(100),
createdBy BIGINT NOT NULL,
createdOn DATETIME NOT NULL,
modifiedOn DATETIME NOT NULL,
modifiedBy	BIGINT NOT NULL,
FOREIGN KEY (accountId) REFERENCES account(accountId)
)


Create table account(
accountId BIGINT IDENTITY(1,1) PRIMARY KEY, 
email VARCHAR(100) NOT NULL,
password VARCHAR(100) NOT NULL,
createdOn DATETIME NOT NULL,
modifiedOn DATETIME NOT NULL,
modifiedBy	BIGINT NOT NULL,
accountStatus INT DEFAULT 0 NOT NULL,
isDeleted BIT NOT NULL
)

CREATE TABLE job(
jobId BIGINT IDENTITY(1,1) PRIMARY KEY,
jobTitle VARCHAR(150) NOT NULL,
jobMinExp INT NOT NULL,
jobMaxExp INT NOT NULL,
jobDisclosed BIT DEFAULT 1 NOT NULL,
jobKeyword TEXT NOT NULL,
jobMinSalary BIGINT NOT NULL,
jobMaxSalary BIGINT NOT NULL,
jobDescription TEXT NOT NULL,
jobOtherDetailPresent BIT DEFAULT 1 NOT NULL,
jobOtherDetail TEXT NOT NULL,
jobCompanyInfo TEXT NOT NULL,
jobApplyModeIsEmail BIT DEFAULT 0 NOT NULL,
jobApplyMode VARCHAR(200) NOT NULL,
jobExpireDate DATETIME NOT NULL,
createdBy	BIGINT NOT NULL,
createdOn DATETIME NOT NULL,
modifiedOn DATETIME NOT NULL,
modifiedBy	BIGINT NOT NULL,
jobStatus INT NOT NULL,
jobDeteled BIT NOT NULL
)



