基于C#和MySQL数据库实现的高校教务管理系统
系统结构
  系统一共设置了三种用户（管理员，教师，学生），实现三层管理结构（管理员管理教师信息，教师管理学生信息，学生则仅是查询自身的信息），简化设计，便于实现。三种用户均通过登录的方式进入系统界面，使用相应的功能。
  管理员端功能：自身账户管理（包括修改密码，注销账号），教师信息管理（包括添加，修改，删除教师），系统反馈信息处理（包括教师反馈和学生反馈），学院信息管理（包括添加，修改，删除学院），毕业要求（包括学分要求和绩点要求）。
  教师端功能：课程信息管理（包括添加，修改，删除课程信息以及授课信息），学生信息管理（包括添加，修改，删除学生），成绩管理（包括设置课程成绩），反馈到管理员，添加专业。
  学生端功能：选课/退课，学生成绩（包括选修课程成绩，平均学分绩点），学生信息（包括学籍信息），学院和专业（包括学院，专业），反馈到管理员。
  系统中创建了许多窗体来作为GUI进行交互，窗体文件如下所示：（Form1为登录界面，其余的见名知意）
  ![image](https://github.com/user-attachments/assets/ce069992-559b-45eb-8390-46cef53a5326)
    图 1 系统窗口界面文件
另外，单独写了一个Dao类来实现数据库连接（Visual Studio中直接连接MySQL有些困难，直接用代码连接容易些。不过注意：需要先装MySQL.Data）。项目中还用res文件夹存放项目资源文件（有几张可以作为界面背景的图片）。项目总体结构如下所示：
  ![image](https://github.com/user-attachments/assets/055fabe7-e0f1-4060-9a8c-50a2160af15e)
    图 2系统项目总体结构
数据库设计
  根据系统需要实现的功能综合分析，一共设计了11张表。如下所示：
  ![image](https://github.com/user-attachments/assets/ad538d8b-630e-4b02-830d-d8359598ce66)
    图 3 系统数据库表
  ![image](https://github.com/user-attachments/assets/8f22fff8-22d5-44d7-9c97-ba2f22e36eef)
    图 4 系统数据库表的具体设计
注意：数据表的设计并没有严格遵循范式规则，有很大改进空间。

设计了一个触发器，用来当学生选了某门课程后将授课表中“选课人数”字段自动加1。触发器的设置不止这么一个，可以有许多，例如修改或删除了某学生的信息后需要同步更新选课表sc，修改或删除了某教师的信息后需要同步更新授课表sk等等，读者可以自行按需求扩展。
设计了一个studentcourseinfo视图，用于快速查询一些有关学生的信息.其中status为该学生这门课程的通过情况。同样，视图也可以按需扩展。
  ![image](https://github.com/user-attachments/assets/718a49b1-4c57-43d1-a9e8-0b77c71c6a91)
    图 5 studentcourseinfo视图
另外，可以设计存储函数来实现特定的查询，例如学生的成绩以及平均学分绩点。这里并没有设计存储过程。可以参考我们小组当时的课设报告，那个版本的系统数据库设计了存储过程。
系统展示
管理员端：
  ![image](https://github.com/user-attachments/assets/9340f737-5743-4e19-b0f1-21403453fe26)
    图 6 管理员主界面和添加教师界面
  ![image](https://github.com/user-attachments/assets/ef56d65a-a42e-47fb-a694-dcae3a804226)
    图 7 修改删除教师界面
  ![image](https://github.com/user-attachments/assets/3e2e6871-8a63-4188-b158-d5418ecbac4b)
    图 8 反馈信息处理界面
  ![image](https://github.com/user-attachments/assets/bf9c869d-a8e8-4e2e-9d89-b11227d94ddf)
    图 9 添加学院和修改/删除学院

  ![image](https://github.com/user-attachments/assets/0a5631c2-864a-4c50-a012-a5d5398b4941)

    图 10 设置毕业要求界面

教师端：

  ![image](https://github.com/user-attachments/assets/b5d9f8e0-7c72-45fa-b747-cc87dd54406b)
    图 11 教师主界面和添加/删除/修改课程界面
  ![image](https://github.com/user-attachments/assets/73eab10f-4146-4693-af3c-402fb05f9f06)
    图 12 添加学生和修改/删除学生界面
![image](https://github.com/user-attachments/assets/391e81e8-5cdd-4661-ae2a-28efde40ccd7)
    图 13 设置课程成绩/反馈信息/添加专业界面
学生端：
  ![image](https://github.com/user-attachments/assets/37359aef-056c-4c5e-a3f6-f58696290fad)
    图 14 学生主界面和选课/退课界面
  ![image](https://github.com/user-attachments/assets/dea53f1c-3110-449b-8c32-5d99fa68d54a)
    图 15 选修课程成绩查询以及反馈信息界面
  ![image](https://github.com/user-attachments/assets/cb6657da-8f70-49fd-8a66-1dd87d366e37)
    图 16 学生基本信息弹窗提示
