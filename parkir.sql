/*
 Navicat Premium Data Transfer

 Source Server         : Local Server 2
 Source Server Type    : MySQL
 Source Server Version : 50725
 Source Host           : localhost:3306
 Source Schema         : parkir

 Target Server Type    : MySQL
 Target Server Version : 50725
 File Encoding         : 65001

 Date: 24/08/2019 13:25:34
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for tbl_mahasiswa
-- ----------------------------
DROP TABLE IF EXISTS `tbl_mahasiswa`;
CREATE TABLE `tbl_mahasiswa` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `nim` varchar(18) NOT NULL,
  `nama` varchar(50) DEFAULT NULL,
  `alamat` varchar(100) DEFAULT NULL,
  `email` varchar(50) DEFAULT NULL,
  `status_id` int(5) DEFAULT NULL,
  `created_at` datetime DEFAULT NULL,
  `created_by` varchar(18) DEFAULT NULL,
  `updated_at` datetime DEFAULT NULL,
  `updated_by` varchar(18) DEFAULT NULL,
  `deleted_at` datetime DEFAULT NULL,
  `deleted_by` varchar(18) DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=latin1 COMMENT='status for deleted';

-- ----------------------------
-- Records of tbl_mahasiswa
-- ----------------------------
BEGIN;
INSERT INTO `tbl_mahasiswa` VALUES (1, '1113004', 'bangkit', 'Malang Raya', '222@errr.com', 1, NULL, NULL, NULL, NULL, NULL, NULL);
INSERT INTO `tbl_mahasiswa` VALUES (2, '1113037', 'risqi', 'Malang Raya', '222@errr.com', 1, NULL, NULL, NULL, NULL, NULL, NULL);
INSERT INTO `tbl_mahasiswa` VALUES (3, '1112003', 'bangkit', 'Malang Raya', '222@errr.com', 0, NULL, NULL, NULL, NULL, NULL, NULL);
INSERT INTO `tbl_mahasiswa` VALUES (4, '1112003', 'bangkit', 'Malang Raya', '222@errr.com', 0, NULL, NULL, NULL, NULL, NULL, NULL);
INSERT INTO `tbl_mahasiswa` VALUES (5, '1112003', 'bangkit', 'Malang Raya', '222@errr.com', 0, NULL, NULL, NULL, NULL, NULL, NULL);
INSERT INTO `tbl_mahasiswa` VALUES (6, '1112003', 'bangkit', 'Malang Raya', '222@errr.com', 0, NULL, NULL, NULL, NULL, NULL, NULL);
INSERT INTO `tbl_mahasiswa` VALUES (7, '1112003', 'bangkit', 'Malang Raya', '222@errr.com', 0, NULL, NULL, NULL, NULL, NULL, NULL);
INSERT INTO `tbl_mahasiswa` VALUES (8, '1112003', 'bangkit', 'Malang res Raya', 'hello@errr.com', 1, NULL, NULL, NULL, NULL, NULL, NULL);
INSERT INTO `tbl_mahasiswa` VALUES (9, '1112004', 'bangkit', 'Malang Raya', '222@aww.com', 1, '2019-08-20 05:08:08', NULL, NULL, NULL, NULL, NULL);
INSERT INTO `tbl_mahasiswa` VALUES (10, '1112005', 'bangkit', 'Malang Raya', '222@aww.com', 1, '2019-08-20 05:08:30', NULL, NULL, NULL, NULL, NULL);
INSERT INTO `tbl_mahasiswa` VALUES (11, '1112006', 'bangkit', 'Malang Raya', '222@aww.com', 1, '2019-08-20 05:08:05', NULL, NULL, NULL, NULL, NULL);
INSERT INTO `tbl_mahasiswa` VALUES (12, '1112007', 'bangkit', 'Malang Raya', '222@aww.com', 1, '2019-08-20 05:08:45', NULL, NULL, NULL, NULL, NULL);
INSERT INTO `tbl_mahasiswa` VALUES (13, '1112008', 'bangkit', 'Malang res Raya', 'hello@errr.com', 0, '2019-08-20 05:08:00', '900009', '2019-08-20 05:08:58', '900009', '2019-08-20 05:08:26', '900009');
COMMIT;

-- ----------------------------
-- Table structure for tbl_parkir
-- ----------------------------
DROP TABLE IF EXISTS `tbl_parkir`;
CREATE TABLE `tbl_parkir` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `nomor_induk` varchar(18) DEFAULT NULL,
  `jenis_parkir` varchar(100) DEFAULT NULL,
  `waktu` datetime DEFAULT NULL,
  `status_id` int(5) DEFAULT NULL,
  `created_at` datetime DEFAULT NULL,
  `created_by` varchar(18) DEFAULT NULL,
  `updated_at` datetime DEFAULT NULL,
  `updated_by` varchar(18) DEFAULT NULL,
  `deleted_at` datetime DEFAULT NULL,
  `deleted_by` varchar(18) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=21 DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of tbl_parkir
-- ----------------------------
BEGIN;
INSERT INTO `tbl_parkir` VALUES (1, '1113004', 'masuk', '0000-00-00 00:00:00', 0, '0000-00-00 00:00:00', NULL, NULL, NULL, NULL, NULL);
INSERT INTO `tbl_parkir` VALUES (2, '1113004', 'masuk', '2019-08-20 08:12:03', 0, '2019-08-20 08:12:03', NULL, NULL, NULL, NULL, NULL);
INSERT INTO `tbl_parkir` VALUES (3, '1113004', 'masuk', '2019-08-20 08:23:51', 0, '2019-08-20 08:23:51', NULL, '2019-08-20 08:34:50', NULL, NULL, NULL);
INSERT INTO `tbl_parkir` VALUES (4, '1113004', 'keluar', '2019-08-20 08:34:42', 1, '2019-08-20 08:34:42', NULL, NULL, NULL, NULL, NULL);
INSERT INTO `tbl_parkir` VALUES (5, '1113004', 'keluar', '2019-08-20 08:34:50', 1, '2019-08-20 08:34:50', NULL, NULL, NULL, NULL, NULL);
INSERT INTO `tbl_parkir` VALUES (6, '1113004', 'masuk', '2019-08-20 08:35:34', 0, '2019-08-20 08:35:34', NULL, '2019-08-20 08:35:51', NULL, NULL, NULL);
INSERT INTO `tbl_parkir` VALUES (7, '1113004', 'keluar', '2019-08-20 08:35:51', 1, '2019-08-20 08:35:51', NULL, NULL, NULL, NULL, NULL);
INSERT INTO `tbl_parkir` VALUES (8, '1113004', 'masuk', '2019-08-20 08:36:24', 2, '2019-08-20 08:36:24', NULL, '2019-08-20 08:37:03', NULL, NULL, NULL);
INSERT INTO `tbl_parkir` VALUES (9, '900009', 'masuk', '2019-08-20 08:36:50', 2, '2019-08-20 08:36:50', NULL, '2019-08-20 08:36:59', NULL, NULL, NULL);
INSERT INTO `tbl_parkir` VALUES (10, '900009', 'keluar', '2019-08-20 08:36:59', 1, '2019-08-20 08:36:59', NULL, NULL, NULL, NULL, NULL);
INSERT INTO `tbl_parkir` VALUES (11, '1113004', 'keluar', '2019-08-20 08:37:03', 1, '2019-08-20 08:37:03', NULL, NULL, NULL, NULL, NULL);
INSERT INTO `tbl_parkir` VALUES (12, '1113004', 'masuk', '2019-08-20 08:37:51', 2, '2019-08-20 08:37:51', NULL, '2019-08-20 08:39:07', NULL, NULL, NULL);
INSERT INTO `tbl_parkir` VALUES (13, '1113004', 'keluar', '2019-08-20 08:39:07', 1, '2019-08-20 08:39:07', NULL, NULL, NULL, NULL, NULL);
INSERT INTO `tbl_parkir` VALUES (14, '1113004', 'masuk', '2019-08-20 08:40:34', 2, '2019-08-20 08:40:34', NULL, '2019-08-20 08:40:45', NULL, NULL, NULL);
INSERT INTO `tbl_parkir` VALUES (15, '1113004', 'keluar', '2019-08-20 08:40:45', 1, '2019-08-20 08:40:45', NULL, NULL, NULL, NULL, NULL);
INSERT INTO `tbl_parkir` VALUES (16, '1113004', 'masuk', '2019-08-20 08:44:46', 2, '2019-08-20 08:08:46', '900009', '2019-08-20 08:08:13', '900009', NULL, NULL);
INSERT INTO `tbl_parkir` VALUES (17, '1113004', 'keluar', '2019-08-20 08:45:13', 1, '2019-08-20 08:08:13', '900009', NULL, NULL, NULL, NULL);
INSERT INTO `tbl_parkir` VALUES (18, '1113004', 'masuk', '2019-08-20 08:45:39', 2, '2019-08-20 08:08:39', '900009', '2019-08-20 08:08:45', '900009', NULL, NULL);
INSERT INTO `tbl_parkir` VALUES (19, '1113004', 'keluar', '2019-08-20 08:45:45', 1, '2019-08-20 08:08:45', '900009', NULL, NULL, NULL, NULL);
INSERT INTO `tbl_parkir` VALUES (20, '1113004', 'masuk', '2019-08-20 08:49:18', 1, '2019-08-20 08:08:18', '900009', NULL, NULL, NULL, NULL);
COMMIT;

-- ----------------------------
-- Table structure for tbl_pegawai
-- ----------------------------
DROP TABLE IF EXISTS `tbl_pegawai`;
CREATE TABLE `tbl_pegawai` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `nip` varchar(18) NOT NULL,
  `nama` varchar(50) DEFAULT NULL,
  `alamat` varchar(100) DEFAULT NULL,
  `email` varchar(50) DEFAULT NULL,
  `status_id` int(5) DEFAULT NULL,
  `created_at` datetime DEFAULT NULL,
  `created_by` varchar(18) DEFAULT NULL,
  `updated_at` datetime DEFAULT NULL,
  `updated_by` varchar(18) DEFAULT NULL,
  `deleted_at` datetime DEFAULT NULL,
  `deleted_by` varchar(18) DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of tbl_pegawai
-- ----------------------------
BEGIN;
INSERT INTO `tbl_pegawai` VALUES (1, '2300011', 'Alun Sujjada', 'Malang', 'alun.sujjada@gmail.com', 1, NULL, NULL, NULL, NULL, NULL, NULL);
INSERT INTO `tbl_pegawai` VALUES (2, '2300012', 'Asep', 'Arjosari', 'asep.123@gmail.com', 1, NULL, NULL, NULL, NULL, NULL, NULL);
INSERT INTO `tbl_pegawai` VALUES (3, '2112003', 'bangkit22', 'Malang res Raya', 'hello@errr.com', 0, NULL, NULL, NULL, NULL, NULL, NULL);
INSERT INTO `tbl_pegawai` VALUES (4, '2112003', 'bangkit', 'Malang Raya', '222@aww.com', 1, NULL, NULL, NULL, NULL, NULL, NULL);
INSERT INTO `tbl_pegawai` VALUES (5, '2112004', 'bangkit22', 'Malang res Raya', 'hello@errr.com', 0, '2019-08-20 05:08:27', '900009', '2019-08-20 05:08:50', '900009', '2019-08-20 05:08:05', '900009');
INSERT INTO `tbl_pegawai` VALUES (6, '2112005', 'bangkit', 'Malang Raya', '222@aww.com', 1, '2019-08-20 05:08:06', '900009', NULL, NULL, NULL, NULL);
INSERT INTO `tbl_pegawai` VALUES (7, '900009', 'Admin', 'Admin', 'admin@sttar.ac.id', 1, NULL, NULL, NULL, NULL, NULL, NULL);
INSERT INTO `tbl_pegawai` VALUES (8, '2000395', 'Bongol 2', 'Malang res Raya', 'hello@errr.com', 1, '2019-08-20 07:08:18', '900009', '2019-08-20 07:08:20', '2300012', NULL, NULL);
INSERT INTO `tbl_pegawai` VALUES (9, '2000396', 'Bongol 2', 'Malang res Raya', 'hello@errr.com', 1, '2019-08-21 02:08:12', '900009', '2019-08-21 02:08:50', '900009', NULL, NULL);
COMMIT;

-- ----------------------------
-- Table structure for tbl_pengguna
-- ----------------------------
DROP TABLE IF EXISTS `tbl_pengguna`;
CREATE TABLE `tbl_pengguna` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `nomor_induk` varchar(18) DEFAULT NULL,
  `no_kartu` varchar(100) DEFAULT NULL,
  `password` varchar(100) DEFAULT NULL,
  `id_type` char(10) DEFAULT NULL,
  `status_id` int(5) DEFAULT NULL,
  `last_logout` datetime DEFAULT NULL,
  `last_login` datetime DEFAULT NULL,
  `created_at` datetime DEFAULT NULL,
  `created_by` int(11) DEFAULT NULL,
  `updated_at` datetime DEFAULT NULL,
  `updated_by` int(11) DEFAULT NULL,
  `deleted_at` datetime DEFAULT NULL,
  `deleted_by` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of tbl_pengguna
-- ----------------------------
BEGIN;
INSERT INTO `tbl_pengguna` VALUES (1, '1113004', '1113004', '202cb962ac59075b964b07152d234b70', '1', 0, '2019-08-10 07:12:36', '2019-08-20 05:53:21', NULL, NULL, '2019-08-20 06:08:01', 900009, '2019-08-20 06:08:21', 900009);
INSERT INTO `tbl_pengguna` VALUES (2, '2300011', '2300011', '202cb962ac59075b964b07152d234b70', '2', 1, NULL, '2019-08-20 07:50:03', NULL, NULL, '2019-08-20 05:08:13', 900009, NULL, NULL);
INSERT INTO `tbl_pengguna` VALUES (3, '2300012', '2300012', '202cb962ac59075b964b07152d234b70', '3', 1, NULL, '2019-08-20 07:12:11', NULL, NULL, '2019-08-20 05:08:13', 900009, NULL, NULL);
INSERT INTO `tbl_pengguna` VALUES (4, '900009', '900009', '202cb962ac59075b964b07152d234b70', '4', 1, NULL, '2019-08-21 20:33:34', NULL, NULL, '2019-08-20 05:08:13', 900009, NULL, NULL);
INSERT INTO `tbl_pengguna` VALUES (5, '2112004', '1113004', '202cb962ac59075b964b07152d234b70', '2', 1, NULL, '2019-08-20 05:53:21', '2019-08-19 16:52:57', 2300012, '2019-08-20 05:08:13', 900009, NULL, NULL);
INSERT INTO `tbl_pengguna` VALUES (6, '1113004', '1113004', '202cb962ac59075b964b07152d234b70', '1', 0, NULL, '2019-08-20 05:53:21', NULL, NULL, '2019-08-20 05:08:43', 900009, NULL, NULL);
INSERT INTO `tbl_pengguna` VALUES (7, '1113004', '1113004', '202cb962ac59075b964b07152d234b70', '1', 0, NULL, '2019-08-20 05:53:21', '2019-08-20 05:08:13', 900009, '2019-08-20 05:08:16', 900009, NULL, NULL);
INSERT INTO `tbl_pengguna` VALUES (8, '1113004', '1113004', '81dc9bdb52d04dc20036dbd8313ed055', '1', 0, NULL, '2019-08-20 06:06:06', '2019-08-20 06:08:00', 900009, '2019-08-20 06:08:40', 900009, '2019-08-20 06:08:09', 900009);
INSERT INTO `tbl_pengguna` VALUES (9, '1113004', '1113004', '202cb962ac59075b964b07152d234b70', '1', 1, NULL, '2019-08-20 07:11:42', '2019-08-20 06:08:18', 900009, NULL, NULL, NULL, NULL);
INSERT INTO `tbl_pengguna` VALUES (10, '2000395', '2000395', '202cb962ac59075b964b07152d234b70', '3', 1, NULL, '2019-08-20 07:09:40', '2019-08-20 07:08:24', 900009, NULL, NULL, NULL, NULL);
COMMIT;

-- ----------------------------
-- Table structure for tbl_type_pengguna
-- ----------------------------
DROP TABLE IF EXISTS `tbl_type_pengguna`;
CREATE TABLE `tbl_type_pengguna` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `keterangan` varchar(15) DEFAULT NULL,
  `status_id` int(5) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of tbl_type_pengguna
-- ----------------------------
BEGIN;
INSERT INTO `tbl_type_pengguna` VALUES (1, 'Mahasiswa', 1);
INSERT INTO `tbl_type_pengguna` VALUES (2, 'Pegawai', 1);
INSERT INTO `tbl_type_pengguna` VALUES (3, 'Satpam', 1);
INSERT INTO `tbl_type_pengguna` VALUES (4, 'Admin', 1);
COMMIT;

-- ----------------------------
-- Table structure for tbl_users_authentication
-- ----------------------------
DROP TABLE IF EXISTS `tbl_users_authentication`;
CREATE TABLE `tbl_users_authentication` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `token` varchar(255) DEFAULT NULL,
  `expired_at` datetime DEFAULT NULL,
  `status_id` int(11) DEFAULT NULL,
  `nomor_induk` varchar(100) NOT NULL,
  `created_at` datetime DEFAULT NULL,
  `updated_at` datetime DEFAULT NULL,
  `deleted_at` datetime DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=55 DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of tbl_users_authentication
-- ----------------------------
BEGIN;
INSERT INTO `tbl_users_authentication` VALUES (1, '$1$ukKVOXtK$qsvpXnj1cVqh5tfZ/tvuf.', '2019-08-09 22:26:32', 1, '1113004', '2019-08-09 04:08:32', NULL, NULL);
INSERT INTO `tbl_users_authentication` VALUES (2, '$1$A/DDeAmb$xKr2IDP6NhmQjOUnQla5J1', '2019-08-09 22:26:48', 1, '1113004', '2019-08-09 04:08:48', NULL, NULL);
INSERT INTO `tbl_users_authentication` VALUES (3, '$1$mNQYX0N3$PmIOEd9JqSzzhrfp6O4r41', '2019-08-09 22:33:56', 2, '1113004', '2019-08-09 04:08:56', NULL, NULL);
INSERT INTO `tbl_users_authentication` VALUES (4, '$1$KbHeIH5f$QbloKFXmnEhWilfzlh4Uo.', '2019-08-10 06:28:17', 2, '1113004', '2019-08-09 06:08:59', '2019-08-09 18:28:17', NULL);
INSERT INTO `tbl_users_authentication` VALUES (5, '$1$RKanIrKQ$alovaP7mJNESMqRGj2Aay0', '2019-08-10 12:44:20', 1, '1113004', '2019-08-10 06:08:20', NULL, NULL);
INSERT INTO `tbl_users_authentication` VALUES (6, '$1$EAHysP7f$nfnC8ddiWFND3axRHWvTP0', '2019-08-10 12:45:39', 1, '1113004', '2019-08-10 06:08:39', NULL, NULL);
INSERT INTO `tbl_users_authentication` VALUES (7, 'eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJub21vcl9pbmR1ayI6IjExMTMwMDQifQ.ceWTIakLQM4UO4Q2VLqYI28e7dGGKvYyh71c1iGz8b0', '2019-08-10 12:51:42', 1, '1113004', '2019-08-10 06:08:42', NULL, NULL);
INSERT INTO `tbl_users_authentication` VALUES (8, 'eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJub21vcl9pbmR1ayI6IjExMTMwMDQiLCJsYXN0X2xvZ2luIjoiMjAxOS0wOC0xMCAwNjo1MzoxNiIsImV4cGlyZWRfYXQiOiIyMDE5LTA4LTEwIDEyOjUzOjE2In0.7b4qUBlzcPGROMxaCRjW_UoQJ-hdaZvViBhx-3nduWw', '2019-08-10 12:53:16', 2, '1113004', '2019-08-10 06:08:16', NULL, NULL);
INSERT INTO `tbl_users_authentication` VALUES (9, 'eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJub21vcl9pbmR1ayI6IjExMTMwMDQiLCJsYXN0X2xvZ2luIjoiMjAxOS0wOC0xMCAwNzoxMjo1MyIsImV4cGlyZWRfYXQiOiIyMDE5LTA4LTEwIDEzOjEyOjUzIn0.WMgqTPYssTKAFKCUKna_Z0TzMiCkAzNnx9Pq3K1THgo', '2019-08-10 13:12:53', 1, '1113004', '2019-08-10 07:08:53', NULL, NULL);
INSERT INTO `tbl_users_authentication` VALUES (10, 'eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJub21vcl9pbmR1ayI6IjExMTMwMDQiLCJsYXN0X2xvZ2luIjoiMjAxOS0wOC0xMCAwNzoxNjoxNyIsImV4cGlyZWRfYXQiOiIyMDE5LTA4LTEwIDEzOjE2OjE3In0.if1mF-tvXgr7L2nt_ZJLOF_67PjW37WRVSRc5afpJSc', '2019-08-10 13:16:17', 1, '1113004', '2019-08-10 07:08:17', NULL, NULL);
INSERT INTO `tbl_users_authentication` VALUES (11, 'eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJub21vcl9pbmR1ayI6IjExMTMwMDQiLCJsYXN0X2xvZ2luIjoiMjAxOS0wOC0xMCAwNzoxNjozMSIsImV4cGlyZWRfYXQiOiIyMDE5LTA4LTEwIDEzOjE2OjMxIn0.et7MtApMFHD_sdxlG1MFNrE_3vX0WyDEoBpFfK70aTU', '2019-08-10 13:16:31', 1, '1113004', '2019-08-10 07:08:31', NULL, NULL);
INSERT INTO `tbl_users_authentication` VALUES (12, 'eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJub21vcl9pbmR1ayI6IjExMTMwMDQiLCJsYXN0X2xvZ2luIjoiMjAxOS0wOC0xMCAwNzoxNzoxMiIsImV4cGlyZWRfYXQiOiIyMDE5LTA4LTEwIDEzOjE3OjEyIn0.dLNZLbxWJSt9-C6eP_SCep3Nmp7JLvtoCMhsEQauUjA', '2019-08-10 13:17:12', 1, '1113004', '2019-08-10 07:08:12', NULL, NULL);
INSERT INTO `tbl_users_authentication` VALUES (13, 'eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJub21vcl9pbmR1ayI6IjExMTMwMDQiLCJsYXN0X2xvZ2luIjoiMjAxOS0wOC0xMCAwNzoxNzo0MCIsImV4cGlyZWRfYXQiOiIyMDE5LTA4LTEwIDEzOjE3OjQwIn0.IqquXflqn2qSAdVX1JdnYdcikd4wOGS0_yHh2TNXyUM', '2019-08-10 13:17:40', 1, '1113004', '2019-08-10 07:08:40', NULL, NULL);
INSERT INTO `tbl_users_authentication` VALUES (14, 'eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJub21vcl9pbmR1ayI6IjExMTMwMDQiLCJsYXN0X2xvZ2luIjoiMjAxOS0wOC0xMCAwNzoxNzo1NSIsImV4cGlyZWRfYXQiOiIyMDE5LTA4LTEwIDEzOjE3OjU1In0.I3eG3e-8kZbO-GhKlUfGRFST673ckXvQPqw5R0fb4Dw', '2019-08-10 13:17:55', 1, '1113004', '2019-08-10 07:08:55', NULL, NULL);
INSERT INTO `tbl_users_authentication` VALUES (15, 'eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJub21vcl9pbmR1ayI6IjExMTMwMDQiLCJsYXN0X2xvZ2luIjoiMjAxOS0wOC0xMCAwNzoxODowNSIsImV4cGlyZWRfYXQiOiIyMDE5LTA4LTEwIDEzOjE4OjA1In0.JqJ3aFDrzwFoQH3cntJrUdKtWFJI56m_21Cjl7orjTk', '2019-08-10 13:18:05', 1, '1113004', '2019-08-10 07:08:05', NULL, NULL);
INSERT INTO `tbl_users_authentication` VALUES (16, 'eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJub21vcl9pbmR1ayI6IjExMTMwMDQiLCJsYXN0X2xvZ2luIjoiMjAxOS0wOC0xMCAwNzoxODo1NCIsImV4cGlyZWRfYXQiOiIyMDE5LTA4LTEwIDEzOjE4OjU0In0.oIFqcsMlikPbTBH1T8DZTjXM4v63C2XhX_ZwlJHKaM4', '2019-08-10 20:15:11', 2, '1113004', '2019-08-10 07:08:54', '2019-08-17 10:34:13', NULL);
INSERT INTO `tbl_users_authentication` VALUES (17, 'eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJub21vcl9pbmR1ayI6IjExMTMwMDQiLCJsYXN0X2xvZ2luIjoiMjAxOS0wOC0xNyAxMDozNDoyNSIsImV4cGlyZWRfYXQiOiIyMDE5LTA4LTE3IDE2OjM0OjI1In0.Q1108D2rz_6F6whVy8Nq9jXQSsZj5KByVYQWtbX0pFY', '2019-08-17 16:34:25', 2, '1113004', '2019-08-17 10:08:25', '2019-08-19 14:28:45', NULL);
INSERT INTO `tbl_users_authentication` VALUES (18, 'eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJub21vcl9pbmR1ayI6IjExMTMwMDQiLCJsYXN0X2xvZ2luIjoiMjAxOS0wOC0xOSAxNDoyODo0MiIsImV4cGlyZWRfYXQiOiIyMDE5LTA4LTE5IDIwOjI4OjQyIn0.abIHKhytBp4bEheGJw7PDsTokp1HkGJpC-DDr9RBHIw', '2019-08-19 20:28:42', 1, '1113004', '2019-08-19 02:08:42', '2019-08-19 14:33:15', NULL);
INSERT INTO `tbl_users_authentication` VALUES (19, 'eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJub21vcl9pbmR1ayI6IjExMTMwMDQiLCJpZF90eXBlIjoiMSIsImxhc3RfbG9naW4iOiIyMDE5LTA4LTE5IDE0OjMzOjQ0IiwiZXhwaXJlZF9hdCI6IjIwMTktMDgtMTkgMjA6MzM6NDQifQ.sD-JF3f1EoSe-EPN6GjDxqjDt3-_dvAuZYObW9ObvXE', '2019-08-19 20:33:44', 1, '1113004', '2019-08-19 02:08:44', '2019-08-19 14:35:07', NULL);
INSERT INTO `tbl_users_authentication` VALUES (20, 'eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJub21vcl9pbmR1ayI6IjExMTMwMDQiLCJpZF90eXBlIjoiMSIsImxhc3RfbG9naW4iOiIyMDE5LTA4LTE5IDE0OjM1OjEzIiwiZXhwaXJlZF9hdCI6IjIwMTktMDgtMTkgMjA6MzU6MTMifQ.FHtSqBlUDRGT8-8ymB8KF0O7N8Dsv15adLL0fb9-ekw', '2019-08-19 20:35:13', 1, '1113004', '2019-08-19 02:08:13', '2019-08-19 16:24:54', NULL);
INSERT INTO `tbl_users_authentication` VALUES (21, 'eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJub21vcl9pbmR1ayI6IjExMTMwMDQiLCJpZF90eXBlIjoiMSIsImxhc3RfbG9naW4iOiIyMDE5LTA4LTE5IDE0OjQ3OjE2IiwiZXhwaXJlZF9hdCI6IjIwMTktMDgtMTkgMjA6NDc6MTYifQ.axi-C40ph_6aJ-MvXisDvLHvXpqakdLUDNAzC1URQlY', '2019-08-19 20:47:16', 1, '1113004', '2019-08-19 02:08:16', '2019-08-19 14:47:25', NULL);
INSERT INTO `tbl_users_authentication` VALUES (22, 'eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJub21vcl9pbmR1ayI6IjIzMDAwMTIiLCJpZF90eXBlIjoiMyIsImxhc3RfbG9naW4iOiIyMDE5LTA4LTE5IDE0OjQ3OjM4IiwiZXhwaXJlZF9hdCI6IjIwMTktMDgtMTkgMjA6NDc6MzgifQ.L2bosg9DCDECLjuX3KqgMjuM4HaBo5CAjJTixvZH7L8', '2019-08-19 20:47:38', 2, '2300012', '2019-08-19 02:08:38', '2019-08-20 01:57:37', NULL);
INSERT INTO `tbl_users_authentication` VALUES (23, 'eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJub21vcl9pbmR1ayI6IjIzMDAwMTIiLCJpZF90eXBlIjoiMyIsImxhc3RfbG9naW4iOiIyMDE5LTA4LTE5IDE2OjI1OjA5IiwiZXhwaXJlZF9hdCI6IjIwMTktMDgtMTkgMjI6MjU6MDkifQ.n1jUVFOxnpI0G50Wy3RW0N5q2REKsaAz1-X1JwAkzXo', '2019-08-19 22:25:09', 1, '2300012', '2019-08-19 04:08:09', '2019-08-19 16:25:56', NULL);
INSERT INTO `tbl_users_authentication` VALUES (24, 'eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJub21vcl9pbmR1ayI6IjIzMDAwMTIiLCJpZF90eXBlIjoiMyIsImxhc3RfbG9naW4iOiIyMDE5LTA4LTE5IDE2OjUxOjUwIiwiZXhwaXJlZF9hdCI6IjIwMTktMDgtMTkgMjI6NTE6NTAifQ.qeru9moQv0v2twNEivSzacQ-2Zi-IHkdN65t7sIr52U', '2019-08-19 22:51:50', 1, '2300012', '2019-08-19 04:08:50', NULL, NULL);
INSERT INTO `tbl_users_authentication` VALUES (25, 'eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJub21vcl9pbmR1ayI6IjIxMTIwMDMiLCJpZF90eXBlIjoiMiIsImxhc3RfbG9naW4iOiIyMDE5LTA4LTE5IDE2OjUzOjQ1IiwiZXhwaXJlZF9hdCI6IjIwMTktMDgtMTkgMjI6NTM6NDUifQ.6K-3JANtMzF7eU6fWIzI5FWJceDZueK7-cQ5hzXU8kM', '2019-08-19 22:53:45', 1, '2112003', '2019-08-19 04:08:45', NULL, NULL);
INSERT INTO `tbl_users_authentication` VALUES (26, 'eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJub21vcl9pbmR1ayI6IjIxMTIwMDMiLCJpZF90eXBlIjoiMiIsImxhc3RfbG9naW4iOiIyMDE5LTA4LTIwIDAxOjU3OjMyIiwiZXhwaXJlZF9hdCI6IjIwMTktMDgtMjAgMDc6NTc6MzIifQ.-Nj09ALtEceuk2gHjOoJndD3JQw4F4kHdTWb4SF7nVM', '2019-08-20 07:57:32', 1, '2112003', '2019-08-20 01:08:32', NULL, NULL);
INSERT INTO `tbl_users_authentication` VALUES (27, 'eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJub21vcl9pbmR1ayI6IjIxMTIwMDMiLCJpZF90eXBlIjoiMiIsImxhc3RfbG9naW4iOiIyMDE5LTA4LTIwIDAyOjQxOjU3IiwiZXhwaXJlZF9hdCI6IjIwMTktMDgtMjAgMDg6NDE6NTcifQ.4dJjDVfq0JMkBNY0xiy57RutDSJDndCDpRVjJuo233M', '2019-08-20 08:41:57', 1, '2112003', '2019-08-20 02:08:57', NULL, NULL);
INSERT INTO `tbl_users_authentication` VALUES (28, 'eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJub21vcl9pbmR1ayI6IjIxMTIwMDMiLCJpZF90eXBlIjoiMiIsImxhc3RfbG9naW4iOiIyMDE5LTA4LTIwIDAyOjQyOjQ5IiwiZXhwaXJlZF9hdCI6IjIwMTktMDgtMjAgMDg6NDI6NDkifQ.4S3StnLxeeayKhC_8zVZkfE-32HTnrA3ohLlO2-sT_M', '2019-08-20 08:42:49', 1, '2112003', '2019-08-20 02:08:49', '2019-08-20 06:41:04', NULL);
INSERT INTO `tbl_users_authentication` VALUES (29, 'eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJub21vcl9pbmR1ayI6IjkwMDAwOSIsImlkX3R5cGUiOiI0IiwibGFzdF9sb2dpbiI6IjIwMTktMDgtMjAgMDQ6NDc6MjYiLCJleHBpcmVkX2F0IjoiMjAxOS0wOC0yMCAxMDo0NzoyNiJ9.BRDNw1L38ktmiOyi9TneWSuHkZHWIvcvWl8SPKgqPrc', '2019-08-20 10:47:26', 1, '900009', '2019-08-20 04:08:26', NULL, NULL);
INSERT INTO `tbl_users_authentication` VALUES (30, 'eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJub21vcl9pbmR1ayI6IjkwMDAwOSIsImlkX3R5cGUiOiI0IiwibGFzdF9sb2dpbiI6IjIwMTktMDgtMjAgMDQ6NDc6NDAiLCJleHBpcmVkX2F0IjoiMjAxOS0wOC0yMCAxMDo0Nzo0MCJ9.6KZAfSeITg3cXcP6z8UyhBc9qXZdUD6C83Xsj_ejtFo', '2019-08-20 10:47:40', 1, '900009', '2019-08-20 04:08:40', '2019-08-20 06:46:17', NULL);
INSERT INTO `tbl_users_authentication` VALUES (31, 'eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJub21vcl9pbmR1ayI6IjkwMDAwOSIsImlkX3R5cGUiOiI0IiwibGFzdF9sb2dpbiI6IjIwMTktMDgtMjAgMDU6MzM6MTgiLCJleHBpcmVkX2F0IjoiMjAxOS0wOC0yMCAxMTozMzoxOCJ9.-j7iAejxgyaMGT7tSVkF46i8OhRIIFZEXSjJArrw_BU', '2019-08-20 11:33:18', 1, '900009', '2019-08-20 05:08:18', '2019-08-20 07:04:10', NULL);
INSERT INTO `tbl_users_authentication` VALUES (32, 'eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJub21vcl9pbmR1ayI6IjExMTMwMDQiLCJpZF90eXBlIjoiMSIsImxhc3RfbG9naW4iOiIyMDE5LTA4LTIwIDA1OjUzOjE4IiwiZXhwaXJlZF9hdCI6IjIwMTktMDgtMjAgMTE6NTM6MTgifQ.ASkCwtUfrBE2Z2EVMHA75Bzua-ogYw687ijGIMRAFsY', '2019-08-20 11:53:18', 1, '1113004', '2019-08-20 05:08:18', NULL, NULL);
INSERT INTO `tbl_users_authentication` VALUES (33, 'eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJub21vcl9pbmR1ayI6IjExMTMwMDQiLCJpZF90eXBlIjoiMSIsImxhc3RfbG9naW4iOiIyMDE5LTA4LTIwIDA1OjUzOjIxIiwiZXhwaXJlZF9hdCI6IjIwMTktMDgtMjAgMTE6NTM6MjEifQ.yxpSvaL8n32DWj4JKjyn2dri5AAAcWBe-253k_wnFns', '2019-08-20 11:53:21', 1, '1113004', '2019-08-20 05:08:21', NULL, NULL);
INSERT INTO `tbl_users_authentication` VALUES (34, 'eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJub21vcl9pbmR1ayI6IjExMTMwMDQiLCJpZF90eXBlIjoiMSIsImxhc3RfbG9naW4iOiIyMDE5LTA4LTIwIDA2OjA1OjU5IiwiZXhwaXJlZF9hdCI6IjIwMTktMDgtMjAgMTI6MDU6NTkifQ.TYTvlyMQs9TlRy0JZN9EmFuunQg4lW0qA3LZeQlMwCY', '2019-08-20 12:05:59', 1, '1113004', '2019-08-20 06:08:59', NULL, NULL);
INSERT INTO `tbl_users_authentication` VALUES (35, 'eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJub21vcl9pbmR1ayI6IjExMTMwMDQiLCJpZF90eXBlIjoiMSIsImxhc3RfbG9naW4iOiIyMDE5LTA4LTIwIDA2OjA2OjA2IiwiZXhwaXJlZF9hdCI6IjIwMTktMDgtMjAgMTI6MDY6MDYifQ.4UXmlIzP9l3_eXQWikGf6Nz3GBAlqPEC-T6iuv2EwXI', '2019-08-20 12:06:06', 1, '1113004', '2019-08-20 06:08:06', NULL, NULL);
INSERT INTO `tbl_users_authentication` VALUES (36, 'eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJub21vcl9pbmR1ayI6IjExMTMwMDQiLCJpZF90eXBlIjoiMSIsImxhc3RfbG9naW4iOiIyMDE5LTA4LTIwIDA2OjA3OjI1IiwiZXhwaXJlZF9hdCI6IjIwMTktMDgtMjAgMTI6MDc6MjUifQ.pmxFWTkIkN2p7jqkJ6skZ51o0qZxms6x3xbAJTajSqU', '2019-08-20 12:07:25', 1, '1113004', '2019-08-20 06:08:25', NULL, NULL);
INSERT INTO `tbl_users_authentication` VALUES (37, 'eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJub21vcl9pbmR1ayI6IjExMTMwMDQiLCJpZF90eXBlIjoiMSIsImxhc3RfbG9naW4iOiIyMDE5LTA4LTIwIDA2OjA4OjMxIiwiZXhwaXJlZF9hdCI6IjIwMTktMDgtMjAgMTI6MDg6MzEifQ.Cr1fZhetBBhu5cM84Yq19vHzFnnJuSAWCzVd2qOAToA', '2019-08-20 12:08:31', 1, '1113004', '2019-08-20 06:08:31', NULL, NULL);
INSERT INTO `tbl_users_authentication` VALUES (38, 'eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJub21vcl9pbmR1ayI6IjExMTMwMDQiLCJpZF90eXBlIjoiMSIsImxhc3RfbG9naW4iOiIyMDE5LTA4LTIwIDA2OjE0OjUxIiwiZXhwaXJlZF9hdCI6IjIwMTktMDgtMjAgMTI6MTQ6NTEifQ.JGlahlxnvibDLbT1F77-ZAM5bfrHw6eVdu8xvC3ugcw', '2019-08-20 12:14:51', 1, '1113004', '2019-08-20 06:08:51', NULL, NULL);
INSERT INTO `tbl_users_authentication` VALUES (39, 'eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJub21vcl9pbmR1ayI6IjExMTMwMDQiLCJpZF90eXBlIjoiMSIsImxhc3RfbG9naW4iOiIyMDE5LTA4LTIwIDA2OjE5OjEwIiwiZXhwaXJlZF9hdCI6IjIwMTktMDgtMjAgMTI6MTk6MTAifQ.G0CvzWYfbHK6poJi1oBBszAW4vHP_w7PCfBUoxu4ks0', '2019-08-20 12:19:10', 1, '1113004', '2019-08-20 06:08:10', NULL, NULL);
INSERT INTO `tbl_users_authentication` VALUES (40, 'eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJub21vcl9pbmR1ayI6IjExMTMwMDQiLCJpZF90eXBlIjoiMSIsImxhc3RfbG9naW4iOiIyMDE5LTA4LTIwIDA2OjE5OjQ5IiwiZXhwaXJlZF9hdCI6IjIwMTktMDgtMjAgMTI6MTk6NDkifQ.sM7ZrBI4Rxz8FXGf5uDKe4jIALkC8YC6jqvwLZ-7nc0', '2019-08-20 12:19:49', 1, '1113004', '2019-08-20 06:08:49', NULL, NULL);
INSERT INTO `tbl_users_authentication` VALUES (41, 'eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJub21vcl9pbmR1ayI6IjExMTMwMDQiLCJpZF90eXBlIjoiMSIsImxhc3RfbG9naW4iOiIyMDE5LTA4LTIwIDA2OjIyOjU0IiwiZXhwaXJlZF9hdCI6IjIwMTktMDgtMjAgMTI6MjI6NTQifQ.AAjvLdm3C4g18N3iQX3tvItDgvI300md8RD0evhrfvE', '2019-08-20 12:22:54', 1, '1113004', '2019-08-20 06:08:54', '2019-08-20 06:24:47', NULL);
INSERT INTO `tbl_users_authentication` VALUES (42, 'eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJub21vcl9pbmR1ayI6IjExMTMwMDQiLCJpZF90eXBlIjoiMSIsImxhc3RfbG9naW4iOiIyMDE5LTA4LTIwIDA2OjI1OjUzIiwiZXhwaXJlZF9hdCI6IjIwMTktMDgtMjAgMTI6MjU6NTMifQ.mLjSuwpxgnn7BiNS1WO__TZWbMQtJUsGUfnStBWXouw', '2019-08-20 12:25:53', 1, '1113004', '2019-08-20 06:08:53', '2019-08-20 06:27:58', NULL);
INSERT INTO `tbl_users_authentication` VALUES (43, 'eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJub21vcl9pbmR1ayI6IjExMTMwMDQiLCJpZF90eXBlIjoiMSIsImxhc3RfbG9naW4iOiIyMDE5LTA4LTIwIDA2OjI4OjIyIiwiZXhwaXJlZF9hdCI6IjIwMTktMDgtMjAgMTI6Mjg6MjIifQ.-kskwr2I7Te17GcRwkvJKksyv3raglDJ5JjncB1AdsE', '2019-08-20 12:28:22', 1, '1113004', '2019-08-20 06:08:22', '2019-08-20 07:07:20', NULL);
INSERT INTO `tbl_users_authentication` VALUES (44, 'eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJub21vcl9pbmR1ayI6IjkwMDAwOSIsImlkX3R5cGUiOiI0IiwibGFzdF9sb2dpbiI6IjIwMTktMDgtMjAgMDc6MDE6MTciLCJleHBpcmVkX2F0IjoiMjAxOS0wOC0yMCAxMzowMToxNyJ9.DHCP2fcbM8CwNEO8qWQ_Tv2yV85stCzOxYqaV31Vsrk', '2019-08-20 13:01:17', 2, '900009', '2019-08-20 07:08:17', '2019-08-21 14:45:42', NULL);
INSERT INTO `tbl_users_authentication` VALUES (45, 'eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJub21vcl9pbmR1ayI6IjIwMDAzOTUiLCJpZF90eXBlIjoiMyIsImxhc3RfbG9naW4iOiIyMDE5LTA4LTIwIDA3OjA4OjI0IiwiZXhwaXJlZF9hdCI6IjIwMTktMDgtMjAgMTM6MDg6MjQifQ.eAlMRyjZJvlHtvFwD8DdHQdoV-cX-2uda3EjGIiHZFc', '2019-08-20 13:08:24', 1, '2000395', '2019-08-20 07:08:24', NULL, NULL);
INSERT INTO `tbl_users_authentication` VALUES (46, 'eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJub21vcl9pbmR1ayI6IjIwMDAzOTUiLCJpZF90eXBlIjoiMyIsImxhc3RfbG9naW4iOiIyMDE5LTA4LTIwIDA3OjA5OjMyIiwiZXhwaXJlZF9hdCI6IjIwMTktMDgtMjAgMTM6MDk6MzIifQ.BX9CWY2NCXFrD9J3yNEtZ-VsOPfhoUnyFANfEsS063I', '2019-08-20 13:09:32', 1, '2000395', '2019-08-20 07:08:32', NULL, NULL);
INSERT INTO `tbl_users_authentication` VALUES (47, 'eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJub21vcl9pbmR1ayI6IjIwMDAzOTUiLCJpZF90eXBlIjoiMyIsImxhc3RfbG9naW4iOiIyMDE5LTA4LTIwIDA3OjA5OjQwIiwiZXhwaXJlZF9hdCI6IjIwMTktMDgtMjAgMTM6MDk6NDAifQ.kFtW50qvCFToppjotp7y2yVWmcA3pgXHjQN5Ct_GBgI', '2019-08-20 13:09:40', 1, '2000395', '2019-08-20 07:08:40', '2019-08-20 07:11:30', NULL);
INSERT INTO `tbl_users_authentication` VALUES (48, 'eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJub21vcl9pbmR1ayI6IjExMTMwMDQiLCJpZF90eXBlIjoiMSIsImxhc3RfbG9naW4iOiIyMDE5LTA4LTIwIDA3OjExOjQyIiwiZXhwaXJlZF9hdCI6IjIwMTktMDgtMjAgMTM6MTE6NDIifQ.FbrcMuuk37rGha2uD2shJelogsbDq8GKzUN0-5bPJhE', '2019-08-20 13:11:42', 1, '1113004', '2019-08-20 07:08:42', '2019-08-20 07:11:56', NULL);
INSERT INTO `tbl_users_authentication` VALUES (49, 'eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJub21vcl9pbmR1ayI6IjIzMDAwMTIiLCJpZF90eXBlIjoiMyIsImxhc3RfbG9naW4iOiIyMDE5LTA4LTIwIDA3OjEyOjExIiwiZXhwaXJlZF9hdCI6IjIwMTktMDgtMjAgMTM6MTI6MTEifQ.MjBBYJtSfR5pH2v5xzhf28gqgAJCe5ipK_4AJhGJQG0', '2019-08-20 13:12:11', 1, '2300012', '2019-08-20 07:08:11', '2019-08-20 07:12:20', NULL);
INSERT INTO `tbl_users_authentication` VALUES (50, 'eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJub21vcl9pbmR1ayI6IjIzMDAwMTEiLCJpZF90eXBlIjoiMiIsImxhc3RfbG9naW4iOiIyMDE5LTA4LTIwIDA3OjEyOjM5IiwiZXhwaXJlZF9hdCI6IjIwMTktMDgtMjAgMTM6MTI6MzkifQ.Jlf7WQBlVAgjL5RI6moKNCCNJA3BwSUV3S60XtKlrWE', '2019-08-20 13:12:39', 1, '2300011', '2019-08-20 07:08:39', '2019-08-20 07:49:56', NULL);
INSERT INTO `tbl_users_authentication` VALUES (51, 'eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJub21vcl9pbmR1ayI6IjIzMDAwMTEiLCJpZF90eXBlIjoiMiIsImxhc3RfbG9naW4iOiIyMDE5LTA4LTIwIDA3OjUwOjAzIiwiZXhwaXJlZF9hdCI6IjIwMTktMDgtMjAgMTM6NTA6MDMifQ.qcaOjFV6B8gCNQeh8fhmutSUUuFrLocyaTgkpgo3N7s', '2019-08-20 13:50:03', 1, '2300011', '2019-08-20 07:08:03', '2019-08-20 07:52:50', NULL);
INSERT INTO `tbl_users_authentication` VALUES (52, 'eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJub21vcl9pbmR1ayI6IjkwMDAwOSIsImlkX3R5cGUiOiI0IiwibGFzdF9sb2dpbiI6IjIwMTktMDgtMjAgMDc6NTA6NDEiLCJleHBpcmVkX2F0IjoiMjAxOS0wOC0yMCAxMzo1MDo0MSJ9.zMLzewWwaFg67H2jSb1GtjcAHB_z6zNR6yTcCVSSLMw', '2019-08-20 13:50:41', 1, '900009', '2019-08-20 07:08:41', '2019-08-20 08:49:18', NULL);
INSERT INTO `tbl_users_authentication` VALUES (53, 'eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJub21vcl9pbmR1ayI6IjkwMDAwOSIsImlkX3R5cGUiOiI0IiwibGFzdF9sb2dpbiI6IjIwMTktMDgtMjEgMTQ6NDM6MzIiLCJleHBpcmVkX2F0IjoiMjAxOS0wOC0yMSAyMDo0MzozMiJ9.ZdCjqTjZW7BX3ma79hp_FNPmcOeny8sVKC7kDE44WqM', '2019-08-21 20:43:32', 2, '900009', '2019-08-21 02:08:32', '2019-08-21 20:56:26', NULL);
INSERT INTO `tbl_users_authentication` VALUES (54, 'eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJub21vcl9pbmR1ayI6IjkwMDAwOSIsImlkX3R5cGUiOiI0IiwibGFzdF9sb2dpbiI6IjIwMTktMDgtMjEgMjA6MzM6MzQiLCJleHBpcmVkX2F0IjoiMjAxOS0wOC0yMiAwMjozMzozNCJ9.oUoP_ogn0ENUyHzCD3IGAZm-OfTmz8dWBWCzr5oCANI', '2019-08-22 02:33:34', 1, '900009', '2019-08-21 08:08:34', '2019-08-21 20:43:43', NULL);
COMMIT;

SET FOREIGN_KEY_CHECKS = 1;
