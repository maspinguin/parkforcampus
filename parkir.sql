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

 Date: 09/08/2019 22:42:43
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for tbl_mahasiswa
-- ----------------------------
DROP TABLE IF EXISTS `tbl_mahasiswa`;
CREATE TABLE `tbl_mahasiswa` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Nim` varchar(18) NOT NULL,
  `Nama` varchar(50) DEFAULT NULL,
  `Alamat` varchar(100) DEFAULT NULL,
  `Email` varchar(50) DEFAULT NULL,
  `StatusId` int(5) DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
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
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Nip` varchar(18) NOT NULL,
  `Nama` varchar(50) DEFAULT NULL,
  `Alamat` varchar(100) DEFAULT NULL,
  `Email` varchar(50) DEFAULT NULL,
  `StatusId` int(5) DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
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
  `StatusId` int(5) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of tbl_pengguna
-- ----------------------------
BEGIN;
INSERT INTO `tbl_pengguna` VALUES (1, '1113004', '1113004', '2f1f291ccdf63d1260f197d69a8de5ab', '1', 1);
INSERT INTO `tbl_pengguna` VALUES (2, '1113037', '1113037', '2f1f291ccdf63d1260f197d69a8de5ab', '1', 1);
INSERT INTO `tbl_pengguna` VALUES (3, '2300011', '2300011', '2f1f291ccdf63d1260f197d69a8de5ab', '2', 1);
INSERT INTO `tbl_pengguna` VALUES (4, '2300012', '2300012', '2f1f291ccdf63d1260f197d69a8de5ab', '3', 1);
COMMIT;

-- ----------------------------
-- Table structure for tbl_type_pengguna
-- ----------------------------
DROP TABLE IF EXISTS `tbl_type_pengguna`;
CREATE TABLE `tbl_type_pengguna` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `keterangan` varchar(15) DEFAULT NULL,
  `StatusId` int(5) DEFAULT NULL,
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

SET FOREIGN_KEY_CHECKS = 1;
