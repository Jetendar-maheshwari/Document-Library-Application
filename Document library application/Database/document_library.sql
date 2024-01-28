-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Host: localhost:8889
-- Generation Time: Jan 28, 2024 at 07:18 PM
-- Server version: 5.7.39
-- PHP Version: 7.4.33

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `document_library`
--

-- --------------------------------------------------------

--
-- Table structure for table `attachment`
--

CREATE TABLE `attachment` (
  `ID` bigint(20) NOT NULL,
  `fileName` varchar(255) NOT NULL,
  `createTime` int(11) DEFAULT NULL,
  `filePath` varchar(255) NOT NULL,
  `mimeType` varchar(255) DEFAULT NULL,
  `downloadCount` int(11) NOT NULL,
  `role` varchar(255) DEFAULT NULL,
  `meta` text
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `attachment`
--

INSERT INTO `attachment` (`ID`, `fileName`, `createTime`, `filePath`, `mimeType`, `downloadCount`, `role`, `meta`) VALUES
(131, 'Test Pdf file.pdf', 1706466886, '129898-Test Pdf file.pdf', 'application/pdf', 0, NULL, NULL),
(132, 'wallpaper.jpg', 1706466903, '680531-wallpaper.jpg', 'image/jpeg', 1, NULL, NULL);

-- --------------------------------------------------------

--
-- Table structure for table `shared_attachment`
--

CREATE TABLE `shared_attachment` (
  `ID` bigint(20) NOT NULL,
  `attachmentID` bigint(20) NOT NULL,
  `sharedID` bigint(20) NOT NULL,
  `createTime` int(11) NOT NULL,
  `expireTime` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Indexes for dumped tables
--

--
-- Indexes for table `attachment`
--
ALTER TABLE `attachment`
  ADD PRIMARY KEY (`ID`);

--
-- Indexes for table `shared_attachment`
--
ALTER TABLE `shared_attachment`
  ADD PRIMARY KEY (`ID`),
  ADD UNIQUE KEY `sharedID` (`sharedID`),
  ADD KEY `attachment_shared_attachment_ID_fr` (`attachmentID`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `attachment`
--
ALTER TABLE `attachment`
  MODIFY `ID` bigint(20) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=134;

--
-- AUTO_INCREMENT for table `shared_attachment`
--
ALTER TABLE `shared_attachment`
  MODIFY `ID` bigint(20) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=21;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `shared_attachment`
--
ALTER TABLE `shared_attachment`
  ADD CONSTRAINT `attachment_shared_attachment_ID_fr` FOREIGN KEY (`attachmentID`) REFERENCES `attachment` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
