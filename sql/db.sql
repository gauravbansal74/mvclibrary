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

CREATE TABLE tutorialCategory(
tutorialCategoryId BIGINT IDENTITY(1,1) PRIMARY KEY,
tutorialCategoryName VARCHAR(100) NOT NULL,
createdBy BIGINT NOT NULL,
createdOn DATETIME NOT NULL,
modifiedOn DATETIME NOT NULL,
modifiedBy	BIGINT NOT NULL,
tutorialCategoryStatus INT DEFAULT 0 NOT NULL,
isDeleted BIT NOT NULL
)



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



