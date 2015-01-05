CREATE TABLE company(
companyId BIGINT PRIMARY KEY IDENTITY(1,1),
companyName VARCHAR(100) NOT NULL, 
companyAbout VARCHAR(200) NOT NULL,
companyWebsite VARCHAR(50) NOT NULL,
createdBy	BIGINT NOT NULL,
createdOn DATETIME NOT NULL,
modifiedOn DATETIME NOT NULL,
modifiedBy	BIGINT NOT NULL,
)

Create table employer(
employerId BIGINT IDENTITY(1,1) PRIMARY KEY, 
companyId BIGINT NOT NULL,
employerFirstName VARCHAR(100),
employerLastName VARCHAR(100),
employerEmail VARCHAR(100) NOT NULL,
employerPhoneNumber VARCHAR(15),
employerPassword VARCHAR(100) NOT NULL,
employerDesignation VARCHAR(100) NOT NULL,
employerKey VARCHAR(100) NOT NULL,
createdOn DATETIME NOT NULL,
modifiedOn DATETIME NOT NULL,
modifiedBy	BIGINT NOT NULL,
accountStatus INT DEFAULT 0 NOT NULL,
isDeleted BIT NOT NULL
)