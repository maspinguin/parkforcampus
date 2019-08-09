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

 Date: 09/08/2019 23:27:32
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
  `StatusId` int(5) DEFAULT NULL,
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
  `last_login` datetime DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of tbl_pengguna
-- ----------------------------
BEGIN;
INSERT INTO `tbl_pengguna` VALUES (1, '1113004', '1113004', '2f1f291ccdf63d1260f197d69a8de5ab', '1', 1, '2019-08-09 16:26:48');
INSERT INTO `tbl_pengguna` VALUES (2, '1113037', '1113037', '2f1f291ccdf63d1260f197d69a8de5ab', '1', 1, NULL);
INSERT INTO `tbl_pengguna` VALUES (3, '2300011', '2300011', '2f1f291ccdf63d1260f197d69a8de5ab', '2', 1, NULL);
INSERT INTO `tbl_pengguna` VALUES (4, '2300012', '2300012', '2f1f291ccdf63d1260f197d69a8de5ab', '3', 1, NULL);
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
  `created_at` datetime DEFAULT NULL,
  `status_id` int(11) DEFAULT NULL,
  `nomor_induk` varchar(100) NOT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of tbl_users_authentication
-- ----------------------------
BEGIN;
INSERT INTO `tbl_users_authentication` VALUES (1, '$1$ukKVOXtK$qsvpXnj1cVqh5tfZ/tvuf.', '2019-08-09 22:26:32', '2019-08-09 04:08:32', 1, '1113004');
INSERT INTO `tbl_users_authentication` VALUES (2, '$1$A/DDeAmb$xKr2IDP6NhmQjOUnQla5J1', '2019-08-09 22:26:48', '2019-08-09 04:08:48', 1, '1113004');
COMMIT;

SET FOREIGN_KEY_CHECKS = 1;
