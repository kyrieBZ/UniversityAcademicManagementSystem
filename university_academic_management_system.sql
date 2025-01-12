/*
 Navicat Premium Data Transfer

 Source Server         : MYSQL
 Source Server Type    : MySQL
 Source Server Version : 80037 (8.0.37)
 Source Host           : localhost:3306
 Source Schema         : university_academic_management_system

 Target Server Type    : MySQL
 Target Server Version : 80037 (8.0.37)
 File Encoding         : 65001

 Date: 12/01/2025 19:20:35
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for administrator
-- ----------------------------
DROP TABLE IF EXISTS `administrator`;
CREATE TABLE `administrator`  (
  `ID` int NOT NULL,
  `Name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `PassWord` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `Sex` varchar(2) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `Age` int NULL DEFAULT NULL,
  `Motto` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of administrator
-- ----------------------------
INSERT INTO `administrator` VALUES (1, 'bz', '123456', '男', 27, 'null');
INSERT INTO `administrator` VALUES (2, '李沿', '234567', '男', 29, 'null');
INSERT INTO `administrator` VALUES (3, '秦明月', '222222', '女', 23, 'null');

-- ----------------------------
-- Table structure for all_course
-- ----------------------------
DROP TABLE IF EXISTS `all_course`;
CREATE TABLE `all_course`  (
  `Cno` int NOT NULL,
  `Cname` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `Cteacher_id` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `Cteacher_name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `Credit` double NULL DEFAULT NULL,
  `Chour` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `Cintroduce` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `College` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  PRIMARY KEY (`Cno`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of all_course
-- ----------------------------
INSERT INTO `all_course` VALUES (1, '高等数学', '2222109011001', '吴建', 5.5, '72学时', '工科基础课程', '理学院');
INSERT INTO `all_course` VALUES (2, '大学物理', '2222109013001', '汪园', 3.5, '64学时', '大学物理学课程', '理学院');
INSERT INTO `all_course` VALUES (3, '线性代数', '2222109011001', '吴建', 2, '48学时', '工程数学基本课程', '理学院');
INSERT INTO `all_course` VALUES (4, '操作系统', '2222107013002', '景亮', 4, '64学时', '计算机基础课程，408之一', '计算机学院');
INSERT INTO `all_course` VALUES (5, '工程制图', '2222106012001', '李欣怡', 4, '64学时', '机械专业建筑专业基础课程', '机械学院');
INSERT INTO `all_course` VALUES (6, '数据结构', '2222107013002', '景亮', 4, '64学时', '计算机基础课程，408之一', '计算机学院');
INSERT INTO `all_course` VALUES (7, '计算机组成原理', '2222107013002', '景亮', 4, '64学时', '计算机基础课程，408之一', '计算机学院');
INSERT INTO `all_course` VALUES (8, '计算机网络', '2222107013002', '景亮', 3, '64学时', '计算机基础课程，408之一', '计算机学院');
INSERT INTO `all_course` VALUES (9, '计算机程序设计语言（C++）', '2222107013001', '杨习', 4, '64学时', '计算机基础程序设计语言', '计算机学院');
INSERT INTO `all_course` VALUES (10, 'Java程序设计', '2222107013001', '杨习', 2, '48学时', '计算机程序设计语言', '计算机学院');
INSERT INTO `all_course` VALUES (11, 'Python程序设计', '2222107013001', '杨习', 2, '48学时', '计算机程序设计语言', '计算机学院');
INSERT INTO `all_course` VALUES (12, '离散数学', '2222107012002', '罗佳', 3, '48学时', '计算机基础必修课程', '计算机学院');
INSERT INTO `all_course` VALUES (13, '数据库原理', '2222107012002', '罗佳', 3, '48学时', '计算机基础必修课程', '计算机学院');
INSERT INTO `all_course` VALUES (14, '软件工程', '2222107012002', '罗佳', 3, '48学时', '计算机基础必修课程', '计算机学院');
INSERT INTO `all_course` VALUES (15, '数字电子技术', '2222107012002', '罗佳', 3, '48学时', '计算机基础必修课程', '计算机学院');
INSERT INTO `all_course` VALUES (16, '物联网原理', '2222107012002', '罗佳', 3.5, '48学时', '物联网工程专业必修课程', '计算机学院');
INSERT INTO `all_course` VALUES (17, '密码学', '2222107013003', 'kelay', 3, '48学时', '信息安全专业必修课程', '计算机学院');
INSERT INTO `all_course` VALUES (18, '数字图像处理', '2222107013003', 'kelay', 2.5, '48学时', '计算机基础选修课程', '计算机学院');
INSERT INTO `all_course` VALUES (19, '微机原理与接口技术', '2222107013003', 'kelay', 3, '48学时', '计算机基础选修课程', '计算机学院');
INSERT INTO `all_course` VALUES (20, '算法设计与分析', '2222107012002', '罗佳', 2, '24学时', '计算机基础课程', '计算机学院');
INSERT INTO `all_course` VALUES (21, '人工智能', '2222107013003', 'kelay', 3, '48学时', '人工智能基础课程', '计算机学院');
INSERT INTO `all_course` VALUES (22, '深度学习', '2222107013003', 'kelay', 4, '64学时', '人工智能基础课程', '计算机学院');
INSERT INTO `all_course` VALUES (23, '人机交互', '2222107013002', '景亮', 3, '48学时', '软件工程基础课程', '计算机学院');
INSERT INTO `all_course` VALUES (24, '生物学', '2222108011001', '李天', 4, '64学时', '自然科学基础课程', '生命科学与技术学院');
INSERT INTO `all_course` VALUES (25, '细胞工程', '2222108011001', '李天', 4, '64学时', '生物技术专业必修课程', '生命科学与技术学院');
INSERT INTO `all_course` VALUES (26, '基因工程', '2222108012001', 'kyre', 4, '64学时', '生物科学专业必修课程', '生命科学与技术学院');
INSERT INTO `all_course` VALUES (27, '有机化学', '2222108012001', 'kyre', 2.5, '48学时', '生物类专业基础课', '生命科学与技术学院');
INSERT INTO `all_course` VALUES (28, '物理化学', '2222108012002', 'duran', 2.5, '48学时', '生物类专业基础课程', '生命科学与技术学院');
INSERT INTO `all_course` VALUES (29, '遗传学', '2222108012003', 'hade', 2.5, '48学时', '生物类专业基础课程', '生命科学与技术学院');
INSERT INTO `all_course` VALUES (30, '药理学', '2222108011001', '李天', 2.5, '32学时', '生物类专业选修课程', '生命科学与技术学院');
INSERT INTO `all_course` VALUES (31, '发酵工程', '2222108012001', 'kyre', 2.5, '32学时', '生物技术专业选修课程', '生命科学与技术学院');
INSERT INTO `all_course` VALUES (32, '抗体疫苗工程', '2222108012002', 'duran', 2.5, '32学时', '生物技术专业选修课程', '生命科学与技术学院');
INSERT INTO `all_course` VALUES (33, '海洋生物技术', '2222108012002', 'duran', 2, '32学时', '生物技术专业选修课程', '生命科学与技术学院');
INSERT INTO `all_course` VALUES (34, '神经生物学', '2222108011001', '李天', 2.5, '32学时', '生物科学专业选修课程', '生命科学与技术学院');
INSERT INTO `all_course` VALUES (35, '进化生物学', '2222108012003', 'hade', 2, '32学时', '生物科学专业选修课程', '生命科学与技术学院');
INSERT INTO `all_course` VALUES (36, '基因治疗技术', '2222108011001', '李天', 2, '32学时', '生物技术专业选修课程', '生命科学与技术学院');
INSERT INTO `all_course` VALUES (37, '生物统计学', '2222108012003', 'hade', 2, '32学时', '生物科学专业选修课程', '生命科学与技术学院');
INSERT INTO `all_course` VALUES (38, '食品生物技术', '2222108011002', '方蓉', 2, '32学时', '生物技术专业选修课程', '生命科学与技术学院');
INSERT INTO `all_course` VALUES (39, '仿生学', '2222108011002', '芳容', 2, '32学时', '生物类专业选修课程', '生命科学与技术专业');
INSERT INTO `all_course` VALUES (40, '生物化学', '2222108011001', '李天', 3, '64学时', '生物类专业基础课程', '生命科学与技术学院');
INSERT INTO `all_course` VALUES (41, '分析化学', '2222108012002', 'duran', 3, '64学时', '生物类专业基础课程', '生命科学与技术学院');
INSERT INTO `all_course` VALUES (42, '免疫生物学', '2222108011002', '方蓉', 2.5, '48学时', '生物科学专业必修课', '生命科学与技术学院');
INSERT INTO `all_course` VALUES (43, '酶工程', '2222108012002', 'duran', 2.5, '48学时', '生物技术专业必修课程', '生命科学与技术学院');
INSERT INTO `all_course` VALUES (44, '机械原理', '2222106012002', '江琳', 4, '64学时', '机械类专业基础课程', '机械学院');
INSERT INTO `all_course` VALUES (45, '机械设计', '2222106013001', '李玉', 4, '64学时', '机械类专业基础课', '机械学院');
INSERT INTO `all_course` VALUES (46, '理论力学', '2222106012001', '李欣怡', 5, '72学时', '机械类专业基础课程', '机械学院');
INSERT INTO `all_course` VALUES (47, '电工技术', '2222106013002', '胡旭', 4, '64学时', '机械类专业基础课程', '机械学院');
INSERT INTO `all_course` VALUES (48, '机械制造工艺学', '2222106011001', '管燕', 3, '48学时', '机械设计制造及其自动化专业选修课程', '机械学院');
INSERT INTO `all_course` VALUES (49, '制造工程数字化技术', '2222106012002', '江琳', 3, '48学时', '机械设计制造及其自动化专业选修课程', '机械学院');
INSERT INTO `all_course` VALUES (50, '微机电系统', '2222106012001', '李欣怡', 2, '32学时', '机械电子工程专业选修课程', '机械学院');
INSERT INTO `all_course` VALUES (51, '压电驱动与微操控机器人', '2222106012001', '李欣怡', 2, '32学时', '机械电子工程专业选修课程', '机械学院');
INSERT INTO `all_course` VALUES (52, '过程装备制造技术', '2222106013001', '李玉', 2.5, '48学时', '过程装备与控制工程专业选修课程', '机械学院');
INSERT INTO `all_course` VALUES (53, '气液压传动', '2222106011002', '郭玮', 3, '48学时', '过程装备与控制工程专业选修课程', '机械学院');
INSERT INTO `all_course` VALUES (54, '传感与测试技术', '2222106012002', '江琳', 3, '48学时', '机械设计制造及自动化专业必修课程', '机械学院');
INSERT INTO `all_course` VALUES (55, '流体力学与液压气动', '2222106013002', '胡旭', 2.5, '48学时', '机械设计制造及自动化专业必修课程', '机械学院');
INSERT INTO `all_course` VALUES (56, '机电系统动力学', '2222106013002', '胡旭', 2.5, '48学时', '机械电子工程专业必修课', '机械学院');
INSERT INTO `all_course` VALUES (57, '机器人电伺服系统', '2222106013001', '李玉', 2, '48学时', '机械电子工程专业必修课程', '机械学院');
INSERT INTO `all_course` VALUES (58, '控制工程基础', '2222106011001', '管燕', 4, '48学时', '过程装备与控制工程专业必修课程', '机械学院');
INSERT INTO `all_course` VALUES (59, '过程装备与系统', '2222106011002', '江琳', 3, '48学时', '过程装备与控制工程专业必修课程', '机械学院');
INSERT INTO `all_course` VALUES (60, '机械振动', '2222106011001', '管燕', 3, '48学时', '机械设计制造及自动化专业选修课程', '机械学院');
INSERT INTO `all_course` VALUES (61, '机电液系统动态分析与设计', '2222106012002', '李欣怡', 2.5, '48学时', '机械电子工程专业选修课程', '机械学院');
INSERT INTO `all_course` VALUES (62, '过程装备集成科学与技术', '2222106011001', '管燕', 2.5, '48学时', '过程装备与控制工程专业选修课程', '机械学院');
INSERT INTO `all_course` VALUES (63, '软件设计模式', '2222107013002', '景亮', 2.5, '48学时', '软件工程专业选修课程', '计算机学院');
INSERT INTO `all_course` VALUES (64, '数学分析', '2222109011001', '吴建', 4, '64学时', '数学与应用数学专业基础课', '理学院');
INSERT INTO `all_course` VALUES (65, '高等线性代数', '2222109011002', '李川', 4, '48学时', '数学与应用数学专业基础课程', '理学院');
INSERT INTO `all_course` VALUES (66, '概率论', '2222109011002', '李川', 4, '64学时', '数学与应用数学专业基础课程', '理学院');
INSERT INTO `all_course` VALUES (67, '数理方法', '2222109013001', '汪园', 4, '64学时', '物理学专业基础课程', '理学院');
INSERT INTO `all_course` VALUES (68, '常微分方程', '2222109011001', '吴建', 3.5, '64学时', '物理学专业基础课程', '理学院');
INSERT INTO `all_course` VALUES (69, '微积分', '2222109011001', '吴建', 5, '72学时', '物理学专业基础课程', '理学院');
INSERT INTO `all_course` VALUES (70, '数值分析', '2222109013002', '黄云磊', 3, '64学时', '数学与应用数学专业必修课程', '理学院');
INSERT INTO `all_course` VALUES (71, '泛函分析', '2222109011001', '吴建', 2.5, '48学时', '数学与应用数学专业必修课程', '理学院');
INSERT INTO `all_course` VALUES (72, '原子物理', '2222109013001', '汪园', 4, '64学时', '物理学专业必修课程', '理学院');
INSERT INTO `all_course` VALUES (73, '量子力学', '2222109012002', '程艳', 4, '48学时', '物理学专业必修课程', '理学院');
INSERT INTO `all_course` VALUES (74, '固体物理', '2222109012001', '郭源', 4, '64学时', '物理学专业必修课程', '理学院');
INSERT INTO `all_course` VALUES (75, '分析几何', '2222109013002', '黄云磊', 3, '48学时', '数学与应用数学专业选修课程', '理学院');
INSERT INTO `all_course` VALUES (76, '微分几何', '2222109013002', '黄云磊', 3, '48学时', '数学与应用数学专业选修课程', '理学院');
INSERT INTO `all_course` VALUES (77, '多元统计', '2222109011002', '李川', 2.5, '48学时', '数学与应用数学专业选修课程', '理学院');
INSERT INTO `all_course` VALUES (78, '量子光学与量子信息导论', '2222109012002', '程艳', 2.5, '48学时', '物理学专业选修课程', '理学院');
INSERT INTO `all_course` VALUES (79, '光学专题', '2222109013001', '汪园', 2.5, '48学时', '物理学专业选修课程', '理学院');
INSERT INTO `all_course` VALUES (80, '应用物理专题', '2222109012001', '郭源', 2.5, '48学时', '物理学专业选修课程', '理学院');

-- ----------------------------
-- Table structure for college
-- ----------------------------
DROP TABLE IF EXISTS `college`;
CREATE TABLE `college`  (
  `COno` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `COname` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `COleader_id` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `COleader_name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `COintroduce` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  PRIMARY KEY (`COno`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of college
-- ----------------------------
INSERT INTO `college` VALUES ('1', '学院', '1', '李', '我');
INSERT INTO `college` VALUES ('222210601', '机械学院', '2222106012001', '李欣怡', '我校为了培养机械设计相关领域的人才而建设的学院，具有机械设计制造及其自动化，机械电子工程以及过程装备与控制工程三个专业。');
INSERT INTO `college` VALUES ('222210701', '计算机学院', '2222107013001', '杨习', '我校为紧跟国家政策培养21世纪高端计算机领域人才而设立的学院，学院共设置计算机科学与技术，信息安全，物联网工程，人工智能，软件工程5个专业。');
INSERT INTO `college` VALUES ('222210801', '生命科学与技术学院', '2222108011001', '李天', '我校为了培养生物领域的人才而建设的学院，设置生物技术，生物科学两个专业');
INSERT INTO `college` VALUES ('222210901', '理学院', '2222109011001', '吴建', '我校为了培养数学物理领域的高端人才而创建的学院，学院设置数学与应用数学，物理学两个专业。');

-- ----------------------------
-- Table structure for course
-- ----------------------------
DROP TABLE IF EXISTS `course`;
CREATE TABLE `course`  (
  `Cno` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Cname` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `Cteacher_id` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `Cteacher_name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `Credit` double NULL DEFAULT NULL,
  `Chour` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `Cintroduce` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  PRIMARY KEY (`Cno`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of course
-- ----------------------------
INSERT INTO `course` VALUES ('0', '哈哈', '2', '李', 3, '学', '课');
INSERT INTO `course` VALUES ('1', '高等数学', '2222109011001', '吴建', 5.5, '72学时', '工科基础课程');
INSERT INTO `course` VALUES ('12', '离散数学', '2222107012002', '罗佳', 3, '48学时', '计算机基础必修课程');
INSERT INTO `course` VALUES ('19', '微机原理与接口技术', '2222107013003', 'kelay', 3, '48学时', '计算机基础选修课程');
INSERT INTO `course` VALUES ('4', '操作系统', '2222107013002', '景亮', 4, '64学时', '计算机基础课程，408之一');

-- ----------------------------
-- Table structure for feedback
-- ----------------------------
DROP TABLE IF EXISTS `feedback`;
CREATE TABLE `feedback`  (
  `FID` int NOT NULL,
  `UID` int NULL DEFAULT NULL,
  `Uname` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `Fdate` datetime NULL DEFAULT NULL,
  `FInfo` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `Ftype` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  PRIMARY KEY (`FID`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of feedback
-- ----------------------------
INSERT INTO `feedback` VALUES (1, 5, '景亮', '2025-01-09 10:21:59', '系统还不错', '教师');
INSERT INTO `feedback` VALUES (2, 8, '汪园', '2025-01-09 10:23:50', '系统界面需要进一步优化', '教师');
INSERT INTO `feedback` VALUES (3, 3, '罗佳', '2025-01-09 10:27:09', '系统提示不够人性化，出错时并没有给出具体解决方案', '教师');
INSERT INTO `feedback` VALUES (4, 1, '杨习', '2025-01-09 10:40:21', '看了系统源码，系统使用直接sql语句进行数据操作，有sql注入风险', '教师');
INSERT INTO `feedback` VALUES (5, 1, '元明', '2025-01-09 10:44:26', '系统使用体验还不错！', '学生');
INSERT INTO `feedback` VALUES (6, 2, 'bz', '2025-01-12 16:17:04', '系统在我的课程还没有出成绩时就把课程的学分给算到已修学分里了！', '学生');

-- ----------------------------
-- Table structure for graduate_request
-- ----------------------------
DROP TABLE IF EXISTS `graduate_request`;
CREATE TABLE `graduate_request`  (
  `ID` int NOT NULL,
  `CreditRequest` double NULL DEFAULT NULL,
  `GPA_Request` double NULL DEFAULT NULL,
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of graduate_request
-- ----------------------------
INSERT INTO `graduate_request` VALUES (1, 120, 2);

-- ----------------------------
-- Table structure for major
-- ----------------------------
DROP TABLE IF EXISTS `major`;
CREATE TABLE `major`  (
  `Mno` int NOT NULL,
  `Mname` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `Mfee` int NULL DEFAULT NULL,
  `Mintroduce` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `Mcono` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  PRIMARY KEY (`Mno`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of major
-- ----------------------------
INSERT INTO `major` VALUES (1, '机械设计制造及其自动化', 5800, '机械设计制造及其自动化主要研究各种工业机械装备及机电产品的设计、制造、运行控制、生产的基本知识和技能，以机械设计与制造为基础，融入计算机、自动控制等技术，实现工程机械自动运行等功能。', '222210601');
INSERT INTO `major` VALUES (2, '机械电子工程', 4800, '机械电子工程涉及机械、电子、信息、计算机、人工智能等诸多领域，主要研习机械工业自动化、电力电子和计算机应用等技术，包括基础理论知识和机械设计制造方法、计算机软硬件应用能力等，从而进行各类机电产品和系统的设计、制造、试验和开发。', '222210601');
INSERT INTO `major` VALUES (3, '过程装备与控制工程', 5500, '过程装备与控制工程主要研究化工、石油等行业使用的风机、压缩机、反应器等过程装备的设计、制造、控制相关的基本知识和技术。', '222210601');
INSERT INTO `major` VALUES (4, '计算机科学与技术', 6200, '计算机科学与技术主要研究计算机的设计与制造，包含计算机软件、硬件的基本理论、技能与方法，进行计算机系统和软件的开发与维护、硬件的组装等。', '222210701');
INSERT INTO `major` VALUES (5, '信息安全', 5100, '信息安全主要研究信息系统、信息安全与保密、网络安全等方面的基本知识和技术，采取各种防护措施，对信息、网络、服务器等进行安全保护等。', '222210701');
INSERT INTO `major` VALUES (6, '物联网工程', 6000, '物联网工程专业是一门涵盖了传感器技术、数据库技术、射频识别技术、嵌入式系统设计，互联网技术以及云计算技术等多技术综合的专业，旨在培养适应以物联网产业需求为重点、具备本专业的知识和工程技术体系结构以及研发设计能力的基础宽、素质高的卓越工程师', '222210701');
INSERT INTO `major` VALUES (7, '人工智能', 6400, '是一个以计算机科学为基础，由计算机、心理学、哲学等多学科交叉融合的交叉学科、新兴学科，研究、开发用于模拟、延伸和扩展人的智能的理论、方法、技术及应用系统的一门新的技术科学，企图了解智能的实质，并生产出一种新的能以人类智能相似的方式做出反应的智能机器，该领域的研究包括机器人、语言识别、图像识别、自然语言处理和专家系统等。', '222210701');
INSERT INTO `major` VALUES (8, '软件工程', 5800, '软件工程专业以计算机科学与技术学科为基础，强调软件开发的工程性，培养学生从事软件需求分析、软件设计、软件测试、软件维护和软件项目管理等工作所必需的基础知识、基本方法和基本技能。毕业后能够在IT行业、科研机构、企事业中从事软件开发、测试、维护和软件项目管理的高级软件工程技术人才', '222210701');
INSERT INTO `major` VALUES (9, '生物技术', 4800, '生物技术主要研习现代生物学和生物技术的基本理论、基本知识和基本技能，包括分子生物学、微生物学、基因工程、发酵工程及细胞工程等方面，主要利用生物体的物质来改进产品、改良植物和动物、或为特殊用途而培养微生物。常见的克隆、基因重组技术、生物疫苗培育皆隶属于此', '222210801');
INSERT INTO `major` VALUES (10, '生物科学', 4800, '生物科学（Biological Sciences）是一门普通高等学校本科专业，属生物科学类专业，基本修业年限为四年，授予理学学位。生物科学是自然科学的重要分支，是人们观察和揭示生命现象、探讨生命本质和发现生命内在规律的科学。\r\n生物科学专业培养具备生物科学的基本理论、基本知识和较强的实验技能，能在科研院所、高等学校及企事业单位等从事科学研究、教学工作及管理工作的生物科学高级专业人才。', '222210801');
INSERT INTO `major` VALUES (11, '数学与应用数学', 5800, '专业学生主要学习数学和应用数学的基础理论、基本方法，受到数学模型、计算机和数学软件方面的基本训练，具有较好的科学素养，初步具备科学研究、教学、解决实际问题及开发软件等方面的基本能力，培养能在科技、教育和经济部门从事研究、教学工作或在生产经营及管理部门从事实际应用、开发研究和管理工作的高级专门人才。', '222210901');
INSERT INTO `major` VALUES (12, '物理学', 5800, '物理学专业培养掌握物理学的基本理论与方法，具有良好的数学基础和实验技能，能在物理学或相关的科学技术领域中从事科研、教学、技术和相关的管理工作的高级专门人才。', '222210901');

-- ----------------------------
-- Table structure for sc
-- ----------------------------
DROP TABLE IF EXISTS `sc`;
CREATE TABLE `sc`  (
  `SCid` int NOT NULL,
  `Sno` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Cno` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `grade` double NULL DEFAULT NULL,
  PRIMARY KEY (`SCid`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sc
-- ----------------------------
INSERT INTO `sc` VALUES (1, '222241807101', '1', 90);
INSERT INTO `sc` VALUES (3, '222241807101', '5', 74);
INSERT INTO `sc` VALUES (4, '222241807101', '4', 88);
INSERT INTO `sc` VALUES (5, '222241807102', '4', 91);
INSERT INTO `sc` VALUES (6, '222241807102', '19', 81);

-- ----------------------------
-- Table structure for sk
-- ----------------------------
DROP TABLE IF EXISTS `sk`;
CREATE TABLE `sk`  (
  `TeachID` int NOT NULL,
  `Cno` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `Cname` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `Tno` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Tname` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `Cintroduce` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `Snum` int NULL DEFAULT NULL,
  PRIMARY KEY (`TeachID`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sk
-- ----------------------------
INSERT INTO `sk` VALUES (1, '4', '操作系统', '2222107013002', '景亮', '计算机基础课程，408之一', 2);
INSERT INTO `sk` VALUES (2, '12', '离散数学', '2222107012002', '罗佳', '计算机基础必修课程', 0);
INSERT INTO `sk` VALUES (3, '1', '高等数学', '2222109011001', '吴建', '工科基础课程', 0);
INSERT INTO `sk` VALUES (4, '19', '微机原理与接口技术', '2222107013003', 'kelay', '计算机基础选修课程', 1);

-- ----------------------------
-- Table structure for student
-- ----------------------------
DROP TABLE IF EXISTS `student`;
CREATE TABLE `student`  (
  `Sno` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ID` int NULL DEFAULT NULL,
  `PassWord` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `Sname` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `Sex` varchar(2) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `Sage` int NULL DEFAULT NULL,
  `Mno` int NULL DEFAULT NULL,
  PRIMARY KEY (`Sno`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of student
-- ----------------------------
INSERT INTO `student` VALUES ('1', 1, '1', '李', '女', 1, 1);
INSERT INTO `student` VALUES ('222241807101', 1, '234567', '元明', '男', 21, 4);
INSERT INTO `student` VALUES ('222241807102', 2, '111', 'bz', '男', 20, 4);
INSERT INTO `student` VALUES ('222241807103', 3, '222', 'linlin', '女', 20, 4);

-- ----------------------------
-- Table structure for teacher
-- ----------------------------
DROP TABLE IF EXISTS `teacher`;
CREATE TABLE `teacher`  (
  `Tno` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ID` int NULL DEFAULT NULL,
  `PassWord` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `Tname` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `Tsex` varchar(2) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `Tage` int NULL DEFAULT NULL,
  `COno` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `IsLeader` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  PRIMARY KEY (`Tno`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of teacher
-- ----------------------------
INSERT INTO `teacher` VALUES ('110', 1, '3', 'k', '男', 1, '3', 'true');
INSERT INTO `teacher` VALUES ('2222106011001', 14, '1001001', '管燕', '女', 33, '222210601', 'false');
INSERT INTO `teacher` VALUES ('2222106011002', 15, '1001002', '郭玮', '男', 45, '222210601', 'false');
INSERT INTO `teacher` VALUES ('2222106012001', 2, '333333', '李欣怡', '女', 32, '222210601', 'true');
INSERT INTO `teacher` VALUES ('2222106012002', 13, '1002002', '江琳', '女', 36, '222210601', 'false');
INSERT INTO `teacher` VALUES ('2222106013001', 16, '3001001', '李玉', '女', 28, '222210601', 'false');
INSERT INTO `teacher` VALUES ('2222106013002', 17, '3001002', '胡旭', '男', 29, '222210601', 'false');
INSERT INTO `teacher` VALUES ('2222107012002', 3, '200210', '罗佳', '女', 26, '222210701', 'false');
INSERT INTO `teacher` VALUES ('2222107013001', 1, '111111', '杨习', '男', 30, '222210701', 'true');
INSERT INTO `teacher` VALUES ('2222107013002', 5, '011010', '景亮', '男', 28, '222210701', 'false');
INSERT INTO `teacher` VALUES ('2222107013003', 6, '771131', 'kelay', '男', 35, '222210701', 'false');
INSERT INTO `teacher` VALUES ('2222108011001', 4, '222222', '李天', '女', 36, '222210801', 'true');
INSERT INTO `teacher` VALUES ('2222108011002', 12, '1002002', '方蓉', '女', 28, '222210801', 'false');
INSERT INTO `teacher` VALUES ('2222108012001', 9, '2001001', 'kyre', '男', 31, '222210801', 'false');
INSERT INTO `teacher` VALUES ('2222108012002', 10, '2002002', 'duran', '男', 34, '222210801', 'false');
INSERT INTO `teacher` VALUES ('2222108012003', 11, '2003003', 'hade', '男', 33, '222210801', 'false');
INSERT INTO `teacher` VALUES ('2222109011001', 7, '567889', '吴建', '男', 34, '222210901', 'true');
INSERT INTO `teacher` VALUES ('2222109011002', 18, '1002002', '李川', '男', 40, '222210901', 'false');
INSERT INTO `teacher` VALUES ('2222109012001', 19, '2001001', '郭源', '男', 45, '222210901', 'false');
INSERT INTO `teacher` VALUES ('2222109012002', 20, '2001002', '程艳', '女', 30, '222210901', 'false');
INSERT INTO `teacher` VALUES ('2222109013001', 8, '111111', '汪园', '女', 44, '222210901', 'false');
INSERT INTO `teacher` VALUES ('2222109013002', 21, '3002002', '黄云磊', '男', 33, '222210901', 'false');

-- ----------------------------
-- View structure for studentcourseinfo
-- ----------------------------
DROP VIEW IF EXISTS `studentcourseinfo`;
CREATE ALGORITHM = UNDEFINED SQL SECURITY DEFINER VIEW `studentcourseinfo` AS select `s`.`Sno` AS `Sno`,`s`.`Sname` AS `StudentName`,`c`.`Cname` AS `CourseName`,`c`.`Cteacher_name` AS `TeacherName`,`sc`.`grade` AS `Grade`,if((`sc`.`grade` >= 60),'通过','未通过') AS `Status`,(case when (`sc`.`grade` >= 90) then (4.0 + ((`sc`.`grade` - 90) / 10.0)) when (`sc`.`grade` >= 80) then (3.0 + ((`sc`.`grade` - 80) / 10.0)) when (`sc`.`grade` >= 70) then (2.0 + ((`sc`.`grade` - 70) / 10.0)) when (`sc`.`grade` >= 60) then (1.0 + ((`sc`.`grade` - 60) / 10.0)) else 0 end) AS `GPA` from ((`sc` join `course` `c` on((`sc`.`Cno` = `c`.`Cno`))) join `student` `s` on((`sc`.`Sno` = `s`.`Sno`)));

-- ----------------------------
-- Triggers structure for table sc
-- ----------------------------
DROP TRIGGER IF EXISTS `UpdateSnumAfterInsert`;
delimiter ;;
CREATE TRIGGER `UpdateSnumAfterInsert` AFTER INSERT ON `sc` FOR EACH ROW BEGIN
    UPDATE sk
    SET Snum = Snum + 1
    WHERE Cno = NEW.Cno; -- 使用 NEW.Cno 来获取插入的记录的课程编号
END
;;
delimiter ;

SET FOREIGN_KEY_CHECKS = 1;
