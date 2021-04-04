# The-Big-Assignment-of-Introduction-to-Software-Engineering

# C#Windows窗体程序开发-音乐播放器

[TOC]



#### 产品名称：

​	《听听鸽》

#### 产品描述：

​	这是一款面向咕咕社区人员的音乐播放软件，如今，越来越多的鸽子们来到了咕咕社区，考虑到鸽子们无聊且枯燥的鸽旅生活，本软件为鸽子们提供了最朴实无华的音乐服务。《听听鸽》能带给你超越鸽生的乐趣。听我的，听听鸽，不要再鸽下去了！！！

#### 产品功能：

1. 支持播放多种类型音乐文件

2. 支持多种播放控制

   ​	四种传统播放模式、快捷切换播放歌曲

3. 能够搜索本地音乐文件

   ​	自己找不到电脑上的音乐文件了？鸽子帮你找

4. 提供账号注册登陆服务

   ​	登陆享更多功能哦

5. 在线听鸽

   ​	海量音乐等你来鸽

6. 能够建立专属歌单

   ​	妈妈再也不怕我找不到喜欢的歌了

7. 能够下载歌曲

   ​	版权先鸽掉，听听鸽就好

#### 数据库设计：

musicinfo

- Name char(200) PRI

- Singer char(200) PRI

- Fileurl varchar(1024)

- Album char(200)


userinfo

- userid char(18) PRI

- password char(255)

- imageurl char(255)


musiclistinfo

- Listname char(255) PRI

- user_id char(18) PRI

- Fileurl char(255)
- musicname char(200) PRI
- musicsinger char(200) PRI
- musicalbum char(200)

user_musiclist_info

- user_id char(18) PRI

- ListName char(255) PRI

#### 开发平台

Microsoft Visual Studio

#### 语言

C#

#### 数据库

MySQL -version:8.0.19

#### 环境搭建

- ##### 安装package.config中标注的所有包

- ##### 必要的引用（包括但不限于）：

  1. ###### COM:Microsoft Shell Controls And Automation

  2. ###### COM:Windows Media Player

  3. ###### Shell32引用属性：嵌入互操作类型=false

- ##### 需要添加App.config文件并修改对应内容：添加\<add\>信息,[]内容必须设置正确

  ```C#
  <configuration>
    <appSettings>
      <add key="DBConnectionString" value="Persist Security Info=False;Database=[数据库名];Server=[数据库地址];Port=3306;User ID=[数据库账号];Password=[数据库密码];Charset=utf8"></add>
    </appSettings>
    ...
  ```

  


