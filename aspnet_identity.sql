-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL,ALLOW_INVALID_DATES';

-- -----------------------------------------------------
-- Schema aspnet_identity
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema aspnet_identity
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `aspnet_identity` DEFAULT CHARACTER SET utf8 ;
USE `aspnet_identity` ;

-- -----------------------------------------------------
-- Table `aspnet_identity`.`aspnet_role`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `aspnet_identity`.`aspnet_role` (
  `Id` VARCHAR(128) NOT NULL,
  `Name` VARCHAR(256) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `aspnet_identity`.`aspnet_user`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `aspnet_identity`.`aspnet_user` (
  `Id` VARCHAR(128) NOT NULL,
  `Email` VARCHAR(256) NULL DEFAULT NULL,
  `EmailConfirmed` TINYINT(4) NOT NULL,
  `PasswordHash` VARCHAR(1024) NULL DEFAULT NULL,
  `SecurityStamp` VARCHAR(1024) NULL DEFAULT NULL,
  `PhoneNumber` VARCHAR(1024) NULL DEFAULT NULL,
  `PhoneNumberConfirmed` TINYINT(4) NOT NULL,
  `TwoFactorEnabled` TINYINT(4) NOT NULL,
  `LockoutEndDateUtc` DATETIME NULL DEFAULT NULL,
  `LockoutEnabled` TINYINT(4) NULL DEFAULT NULL,
  `AccessFailedCount` INT(11) NOT NULL,
  `UserName` VARCHAR(45) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`),
  INDEX `UserNameIndex` (`UserName` ASC))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `aspnet_identity`.`aspnet_user_claim`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `aspnet_identity`.`aspnet_user_claim` (
  `Id` INT(11) NOT NULL AUTO_INCREMENT,
  `UserId` VARCHAR(128) NOT NULL,
  `ClaimType` VARCHAR(1024) NULL DEFAULT NULL,
  `ClaimValue` VARCHAR(1024) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`),
  INDEX `ix_UserId` (`UserId` ASC),
  CONSTRAINT `fk_aspnet_user_claim_aspnet_user1`
    FOREIGN KEY (`UserId`)
    REFERENCES `aspnet_identity`.`aspnet_user` (`Id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `aspnet_identity`.`aspnet_user_login`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `aspnet_identity`.`aspnet_user_login` (
  `LoginProvider` VARCHAR(128) NOT NULL,
  `ProviderKey` VARCHAR(128) NOT NULL,
  `UserId` VARCHAR(128) NOT NULL,
  PRIMARY KEY (`LoginProvider`, `ProviderKey`, `UserId`),
  INDEX `fk_aspnet_user_login_aspnet_user1_idx` (`UserId` ASC),
  CONSTRAINT `fk_aspnet_user_login_aspnet_user1`
    FOREIGN KEY (`UserId`)
    REFERENCES `aspnet_identity`.`aspnet_user` (`Id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `aspnet_identity`.`aspnet_user_role`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `aspnet_identity`.`aspnet_user_role` (
  `UserId` VARCHAR(128) NOT NULL,
  `RoleId` VARCHAR(128) NOT NULL,
  PRIMARY KEY (`UserId`, `RoleId`),
  INDEX `fk_aspnet_user_role_aspnet_role_idx` (`RoleId` ASC),
  CONSTRAINT `fk_aspnet_user_role_aspnet_role`
    FOREIGN KEY (`RoleId`)
    REFERENCES `aspnet_identity`.`aspnet_role` (`Id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_aspnet_user_role_aspnet_user1`
    FOREIGN KEY (`UserId`)
    REFERENCES `aspnet_identity`.`aspnet_user` (`Id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `aspnet_identity`.`application_role`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `aspnet_identity`.`application_role` (
  `RoleId` VARCHAR(128) NOT NULL,
  `Description` VARCHAR(256) NULL,
  PRIMARY KEY (`RoleId`),
  CONSTRAINT `fk_application_role_aspnet_role1`
    FOREIGN KEY (`RoleId`)
    REFERENCES `aspnet_identity`.`aspnet_role` (`Id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `aspnet_identity`.`application_user`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `aspnet_identity`.`application_user` (
  `UserId` VARCHAR(128) NOT NULL,
  PRIMARY KEY (`UserId`),
  CONSTRAINT `fk_application_user_aspnet_user1`
    FOREIGN KEY (`UserId`)
    REFERENCES `aspnet_identity`.`aspnet_user` (`Id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
