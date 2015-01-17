Create table account(
accountId BIGINT IDENTITY(1,1) PRIMARY KEY, 
email VARCHAR(100) NOT NULL,
password VARCHAR(100) NOT NULL,
firstName VARCHAR(100),
lastName VARCHAR(100),
phoneNumber VARCHAR(15),
basedIn VARCHAR(100),
classifiedRole VARCHAR(150),
jobseekingStatus VARCHAR(100),
availability VARCHAR(50),
highestEducation VARCHAR(100),
workType VARCHAR(100),
Preferredlocations VARCHAR(100),
ResumeFileName VARCHAR(100),
PersonalInformationUpdated BIT DEFAULT 0 NOT NULL,
QualificationSkillsUpdated BIT DEFAULT 0 NOT NULL,
CurrentStatusUpdated BIT DEFAULT 0 NOT NULL,
ResumeUpdated BIT DEFAULT 0 NOT NULL,
accountKey VARCHAR(100) NOT NULL,
createdOn DATETIME NOT NULL,
modifiedOn DATETIME NOT NULL,
modifiedBy	BIGINT NOT NULL,
accountStatus INT DEFAULT 0 NOT NULL,
isDeleted BIT NOT NULL
)

CREATE TABLE BankDetail(
BankDetailId BIGINT IDENTITY(1,1) PRIMARY KEY,
accountId BIGINT NOT NULL,
BankAccountName VARCHAR(100) NOT NULL,
BankAccountNumber VARCHAR(100) NOT NULL,
BankIFSCCode VARCHAR(50) NOT NULL,
createdBy BIGINT NOT NULL,
createdOn DATETIME NOT NULL,
modifiedOn DATETIME NOT NULL,
modifiedBy	BIGINT NOT NULL,
isDeleted BIT DEFAULT 0 NOT NULL,
FOREIGN KEY (accountId) REFERENCES account(accountId)
)

CREATE TABLE wallet(
walletId BIGINT IDENTITY(1,1) PRIMARY KEY,
accountId BIGINT NOT NULL,
walletBalance VARCHAR(100) DEFAULT '0' NOT NULL,
createdBy BIGINT NOT NULL,
createdOn DATETIME NOT NULL,
modifiedOn DATETIME NOT NULL,
modifiedBy	BIGINT NOT NULL,
isDeleted BIT NOT NULL,
FOREIGN KEY (accountId) REFERENCES account(accountId)
)

CREATE TABLE walletTransaction(
walletTransactionId BIGINT IDENTITY(1,1) PRIMARY KEY,
walletId BIGINT NOT NULL,
walletDescription VARCHAR(100) NOT NULL,
transactionAmount VARCHAR(100) NOT NULL,
createdBy BIGINT NOT NULL,
createdOn DATETIME NOT NULL,
modifiedOn DATETIME NOT NULL,
modifiedBy	BIGINT NOT NULL,
transactionStatus INT DEFAULT 0 NOT NULL,
isDeleted BIT NOT NULL,
FOREIGN KEY (walletId) REFERENCES wallet(walletId)
)

CREATE TABLE withdrawal(
withdrawalId BIGINT IDENTITY(1,1) PRIMARY KEY,
accountId BIGINT NOT NULL,
walletId BIGINT NOT NULL,
walletBalance VARCHAR(100) DEFAULT '0' NOT NULL,
createdBy BIGINT NOT NULL,
createdOn DATETIME NOT NULL,
modifiedOn DATETIME NOT NULL,
modifiedBy	BIGINT NOT NULL,
isProcessed BIT NOT NULL,
FOREIGN KEY (accountId) REFERENCES account(accountId),
FOREIGN KEY (walletId) REFERENCES wallet(walletId)
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
jobLocation VARCHAR(200) NOT NULL,
jobDescription TEXT NOT NULL,
jobOtherDetailPresent BIT DEFAULT 1 NOT NULL,
jobOtherDetail TEXT NOT NULL,
jobCompnayName VARCHAR(100)NOT NULL,
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

CREATE TABLE applyJob(
applyJobId BIGINT IDENTITY(1,1) PRIMARY KEY NOT NULL,
accountId BIGINT NOT NULL,
jobId BIGINT NOT NULL,
createdBy BIGINT NOT NULL,
createdOn DATETIME NOT NULL,
modifiedOn DATETIME NOT NULL,
modifiedBy	BIGINT NOT NULL,
isDeleted BIT NOT NULL,
FOREIGN KEY (accountId) REFERENCES account(accountId),
FOREIGN KEY (jobId) REFERENCES job(jobId)
)


CREATE TABLE highestEducation(
highestEducationId BIGINT IDENTITY(1,1) PRIMARY KEY,
highestEducationName VARCHAR(100) NOT NULL,
createdBy BIGINT NOT NULL,
createdOn DATETIME NOT NULL,
modifiedOn DATETIME NOT NULL,
modifiedBy	BIGINT NOT NULL,
isDeleted BIT NOT NULL,
)

INSERT INTO highestEducation (highestEducationName, createdBy, createdOn, modifiedBy, modifiedOn, isDeleted) VALUES ('Completed Year 9-11',1,GETDATE(),1,GETDATE(), 0);
INSERT INTO highestEducation (highestEducationName, createdBy, createdOn, modifiedBy, modifiedOn, isDeleted) VALUES ('Completed High School (Year 12)',1,GETDATE(),1,GETDATE(),0);
INSERT INTO highestEducation (highestEducationName, createdBy, createdOn, modifiedBy, modifiedOn, isDeleted) VALUES ('Diploma',1,GETDATE(),1,GETDATE(),0);
INSERT INTO highestEducation (highestEducationName, createdBy, createdOn, modifiedBy, modifiedOn, isDeleted) VALUES ('TAFE/Trade Certificate',1,GETDATE(),1,GETDATE(),0);
INSERT INTO highestEducation (highestEducationName, createdBy, createdOn, modifiedBy, modifiedOn, isDeleted) VALUES ('Undergraduate',1,GETDATE(),1,GETDATE(),0);
INSERT INTO highestEducation (highestEducationName, createdBy, createdOn, modifiedBy, modifiedOn, isDeleted) VALUES ('Post Graduate Degree',1,GETDATE(),1,GETDATE(),0);
INSERT INTO highestEducation (highestEducationName, createdBy, createdOn, modifiedBy, modifiedOn, isDeleted) VALUES ('Masters',1,GETDATE(),1,GETDATE(),0);
INSERT INTO highestEducation (highestEducationName, createdBy, createdOn, modifiedBy, modifiedOn, isDeleted) VALUES ('PhD',1,GETDATE(),1,GETDATE(),0);
INSERT INTO highestEducation (highestEducationName, createdBy, createdOn, modifiedBy, modifiedOn, isDeleted) VALUES ('Not specified',1,GETDATE(),1,GETDATE(),0);


CREATE TABLE tutorialCategory(
tutorialCategoryId BIGINT IDENTITY(1,1) PRIMARY KEY,
tutorialCategoryName VARCHAR(100) NOT NULL,
tutorialCategoryFileName VARCHAR(100) NOT NULL,
createdBy BIGINT NOT NULL,
createdOn DATETIME NOT NULL,
modifiedOn DATETIME NOT NULL,
modifiedBy	BIGINT NOT NULL,
tutorialCategoryStatus INT DEFAULT 0 NOT NULL,
isDeleted BIT NOT NULL
)

CREATE TABLE video(
videoId BIGINT IDENTITY(1,1) PRIMARY KEY,
categoryId BIGINT NOT NULL,
videoTitle VARCHAR(300) NOT NULL,
videoYoutubeId VARCHAR(100) NOT NULL,
createdBy BIGINT NOT NULL,
createdOn DATETIME NOT NULL,
modifiedOn DATETIME NOT NULL,
modifiedBy	BIGINT NOT NULL,
isDeleted BIT NOT NULL,
FOREIGN KEY (categoryId) REFERENCES tutorialCategory(tutorialCategoryId)
)

ALTER TABLE dbo.account ADD isAdmin BIT DEFAULT 0 NOT NULL;

ALTER TABLE dbo.job ADD FOREIGN KEY (createdBy) REFERENCES account(accountId);


CREATE TABLE Rating(
RatingId BIGINT IDENTITY(1,1) PRIMARY KEY,
RatingValue BIGINT NOT NULL,
JobId BIGINT NOT NULL,
createdBy BIGINT NOT NULL,
createdOn DATETIME NOT NULL,
modifiedOn DATETIME NOT NULL,
modifiedBy	BIGINT NOT NULL,
FOREIGN KEY (JobId) REFERENCES job(jobId),
FOREIGN KEY (createdBy) REFERENCES account(accountId)
)



--------------------------------------------------------------
--------------------------------------------------------------
--Employer Side 

CREATE TABLE company(
companyId BIGINT PRIMARY KEY IDENTITY(1,1),
companyName VARCHAR(100) NOT NULL, 
companyAbout VARCHAR(200) NOT NULL,
companyLogo VARCHAR(100) NOT NULL,
companyWebsite VARCHAR(50) NOT NULL,
createdBy	BIGINT NOT NULL,
createdOn DATETIME NOT NULL,
modifiedOn DATETIME NOT NULL,
modifiedBy	BIGINT NOT NULL
)

ALTER TABLE dbo.account ADD isEmployer BIT DEFAULT 0 NOT NULL;
ALTER TABLE dbo.account ADD isEmployerAdmin BIT DEFAULT 0 NOT NULL;
ALTER TABLE dbo.account ADD companyId BIGINT NULL;
ALTER TABLE dbo.account ADD isEmployerVerified BIT DEFAULT 0 NOT NULL;