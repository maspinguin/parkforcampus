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

 Date: 10/08/2019 15:18:57
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
  PRIMARY KEY (`id`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1 COMMENT='status for deleted';

-- ----------------------------
-- Records of tbl_mahasiswa
-- ----------------------------
BEGIN;
INSERT INTO `tbl_mahasiswa` VALUES (1, '1113004', 'Bangkit Ilham', 'Malang', 'bangkit@wallextech.com', 1);
INSERT INTO `tbl_mahasiswa` VALUES (2, '1113037', 'Risqi Ardiansyah', 'Malang', 'telupitu.ardiansyah@gmail.com', 1);
COMMIT;

-- ----------------------------
-- Table structure for tbl_parkir
-- ----------------------------
DROP TABLE IF EXISTS `tbl_parkir`;
CREATE TABLE `tbl_parkir` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `id_pengguna` varchar(18) DEFAULT NULL,
  `jenis_parkir` varchar(100) DEFAULT NULL,
  `tanggal` date DEFAULT NULL,
  `jam` time(6) DEFAULT NULL,
  `status_id` int(5) DEFAULT NULL,
  `created_at` datetime DEFAULT NULL,
  `updated_at` datetime DEFAULT NULL,
  `deleted_at` datetime DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

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
  PRIMARY KEY (`id`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of tbl_pegawai
-- ----------------------------
BEGIN;
INSERT INTO `tbl_pegawai` VALUES (1, '2300011', 'Alun Sujjada', 'Malang', 'alun.sujjada@gmail.com', 1);
INSERT INTO `tbl_pegawai` VALUES (2, '2300012', 'Asep', 'Arjosari', 'asep.123@gmail.com', 1);
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
  `updated_at` datetime DEFAULT NULL,
  `deleted_at` datetime DEFAULT NULL,
  `created_by` int(11) DEFAULT NULL,
  `updated_by` int(11) DEFAULT NULL,
  `deleted_by` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of tbl_pengguna
-- ----------------------------
BEGIN;
INSERT INTO `tbl_pengguna` VALUES (1, '1113004', '1113004', '2f1f291ccdf63d1260f197d69a8de5ab', '1', 1, '2019-08-10 07:12:36', '2019-08-10 07:18:54', NULL, NULL, NULL, NULL, NULL, NULL);
INSERT INTO `tbl_pengguna` VALUES (2, '1113037', '1113037', '2f1f291ccdf63d1260f197d69a8de5ab', '1', 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
INSERT INTO `tbl_pengguna` VALUES (3, '2300011', '2300011', '2f1f291ccdf63d1260f197d69a8de5ab', '2', 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
INSERT INTO `tbl_pengguna` VALUES (4, '2300012', '2300012', '2f1f291ccdf63d1260f197d69a8de5ab', '3', 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
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
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of tbl_type_pengguna
-- ----------------------------
BEGIN;
INSERT INTO `tbl_type_pengguna` VALUES (1, 'Mahasiswa', 1);
INSERT INTO `tbl_type_pengguna` VALUES (2, 'Pegawai/Dosen', 1);
INSERT INTO `tbl_type_pengguna` VALUES (3, 'Satpam', 1);
COMMIT;

-- ----------------------------
-- Table structure for tbl_users_authentication
-- ----------------------------
DROP TABLE IF EXISTS `tbl_users_authentication`;
CREATE TABLE `tbl_users_authentication` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `token` varchar(255) DEFAULT NULL,
  `expired_at` datetime DEFAULT NULL,
  `updated_at` datetime DEFAULT NULL,
  `created_at` datetime DEFAULT NULL,
  `status_id` int(11) DEFAULT NULL,
  `nomor_induk` varchar(100) NOT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=17 DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of tbl_users_authentication
-- ----------------------------
BEGIN;
INSERT INTO `tbl_users_authentication` VALUES (1, '$1$ukKVOXtK$qsvpXnj1cVqh5tfZ/tvuf.', '2019-08-09 22:26:32', NULL, '2019-08-09 04:08:32', 1, '1113004');
INSERT INTO `tbl_users_authentication` VALUES (2, '$1$A/DDeAmb$xKr2IDP6NhmQjOUnQla5J1', '2019-08-09 22:26:48', NULL, '2019-08-09 04:08:48', 1, '1113004');
INSERT INTO `tbl_users_authentication` VALUES (3, '$1$mNQYX0N3$PmIOEd9JqSzzhrfp6O4r41', '2019-08-09 22:33:56', NULL, '2019-08-09 04:08:56', 2, '1113004');
INSERT INTO `tbl_users_authentication` VALUES (4, '$1$KbHeIH5f$QbloKFXmnEhWilfzlh4Uo.', '2019-08-10 06:28:17', '2019-08-09 18:28:17', '2019-08-09 06:08:59', 2, '1113004');
INSERT INTO `tbl_users_authentication` VALUES (5, '$1$RKanIrKQ$alovaP7mJNESMqRGj2Aay0', '2019-08-10 12:44:20', NULL, '2019-08-10 06:08:20', 1, '1113004');
INSERT INTO `tbl_users_authentication` VALUES (6, '$1$EAHysP7f$nfnC8ddiWFND3axRHWvTP0', '2019-08-10 12:45:39', NULL, '2019-08-10 06:08:39', 1, '1113004');
INSERT INTO `tbl_users_authentication` VALUES (7, 'eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJub21vcl9pbmR1ayI6IjExMTMwMDQifQ.ceWTIakLQM4UO4Q2VLqYI28e7dGGKvYyh71c1iGz8b0', '2019-08-10 12:51:42', NULL, '2019-08-10 06:08:42', 1, '1113004');
INSERT INTO `tbl_users_authentication` VALUES (8, 'eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJub21vcl9pbmR1ayI6IjExMTMwMDQiLCJsYXN0X2xvZ2luIjoiMjAxOS0wOC0xMCAwNjo1MzoxNiIsImV4cGlyZWRfYXQiOiIyMDE5LTA4LTEwIDEyOjUzOjE2In0.7b4qUBlzcPGROMxaCRjW_UoQJ-hdaZvViBhx-3nduWw', '2019-08-10 12:53:16', NULL, '2019-08-10 06:08:16', 2, '1113004');
INSERT INTO `tbl_users_authentication` VALUES (9, 'eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJub21vcl9pbmR1ayI6IjExMTMwMDQiLCJsYXN0X2xvZ2luIjoiMjAxOS0wOC0xMCAwNzoxMjo1MyIsImV4cGlyZWRfYXQiOiIyMDE5LTA4LTEwIDEzOjEyOjUzIn0.WMgqTPYssTKAFKCUKna_Z0TzMiCkAzNnx9Pq3K1THgo', '2019-08-10 13:12:53', NULL, '2019-08-10 07:08:53', 1, '1113004');
INSERT INTO `tbl_users_authentication` VALUES (10, 'eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJub21vcl9pbmR1ayI6IjExMTMwMDQiLCJsYXN0X2xvZ2luIjoiMjAxOS0wOC0xMCAwNzoxNjoxNyIsImV4cGlyZWRfYXQiOiIyMDE5LTA4LTEwIDEzOjE2OjE3In0.if1mF-tvXgr7L2nt_ZJLOF_67PjW37WRVSRc5afpJSc', '2019-08-10 13:16:17', NULL, '2019-08-10 07:08:17', 1, '1113004');
INSERT INTO `tbl_users_authentication` VALUES (11, 'eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJub21vcl9pbmR1ayI6IjExMTMwMDQiLCJsYXN0X2xvZ2luIjoiMjAxOS0wOC0xMCAwNzoxNjozMSIsImV4cGlyZWRfYXQiOiIyMDE5LTA4LTEwIDEzOjE2OjMxIn0.et7MtApMFHD_sdxlG1MFNrE_3vX0WyDEoBpFfK70aTU', '2019-08-10 13:16:31', NULL, '2019-08-10 07:08:31', 1, '1113004');
INSERT INTO `tbl_users_authentication` VALUES (12, 'eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJub21vcl9pbmR1ayI6IjExMTMwMDQiLCJsYXN0X2xvZ2luIjoiMjAxOS0wOC0xMCAwNzoxNzoxMiIsImV4cGlyZWRfYXQiOiIyMDE5LTA4LTEwIDEzOjE3OjEyIn0.dLNZLbxWJSt9-C6eP_SCep3Nmp7JLvtoCMhsEQauUjA', '2019-08-10 13:17:12', NULL, '2019-08-10 07:08:12', 1, '1113004');
INSERT INTO `tbl_users_authentication` VALUES (13, 'eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJub21vcl9pbmR1ayI6IjExMTMwMDQiLCJsYXN0X2xvZ2luIjoiMjAxOS0wOC0xMCAwNzoxNzo0MCIsImV4cGlyZWRfYXQiOiIyMDE5LTA4LTEwIDEzOjE3OjQwIn0.IqquXflqn2qSAdVX1JdnYdcikd4wOGS0_yHh2TNXyUM', '2019-08-10 13:17:40', NULL, '2019-08-10 07:08:40', 1, '1113004');
INSERT INTO `tbl_users_authentication` VALUES (14, 'eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJub21vcl9pbmR1ayI6IjExMTMwMDQiLCJsYXN0X2xvZ2luIjoiMjAxOS0wOC0xMCAwNzoxNzo1NSIsImV4cGlyZWRfYXQiOiIyMDE5LTA4LTEwIDEzOjE3OjU1In0.I3eG3e-8kZbO-GhKlUfGRFST673ckXvQPqw5R0fb4Dw', '2019-08-10 13:17:55', NULL, '2019-08-10 07:08:55', 1, '1113004');
INSERT INTO `tbl_users_authentication` VALUES (15, 'eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJub21vcl9pbmR1ayI6IjExMTMwMDQiLCJsYXN0X2xvZ2luIjoiMjAxOS0wOC0xMCAwNzoxODowNSIsImV4cGlyZWRfYXQiOiIyMDE5LTA4LTEwIDEzOjE4OjA1In0.JqJ3aFDrzwFoQH3cntJrUdKtWFJI56m_21Cjl7orjTk', '2019-08-10 13:18:05', NULL, '2019-08-10 07:08:05', 1, '1113004');
INSERT INTO `tbl_users_authentication` VALUES (16, 'eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJub21vcl9pbmR1ayI6IjExMTMwMDQiLCJsYXN0X2xvZ2luIjoiMjAxOS0wOC0xMCAwNzoxODo1NCIsImV4cGlyZWRfYXQiOiIyMDE5LTA4LTEwIDEzOjE4OjU0In0.oIFqcsMlikPbTBH1T8DZTjXM4v63C2XhX_ZwlJHKaM4', '2019-08-10 20:15:11', '2019-08-10 08:15:53', '2019-08-10 07:08:54', 1, '1113004');
COMMIT;

SET FOREIGN_KEY_CHECKS = 1;
