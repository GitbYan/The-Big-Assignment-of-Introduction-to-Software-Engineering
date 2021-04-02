# The-Big-Assignment-of-Introduction-to-Software-Engineering

# C#Windows窗体程序开发-音乐播放器

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

#### 设计

##### 数据库设计：

-音乐表(musicinfo):

​	名称(Name)

​	歌手(Singer)

​	专辑(Album)

​	音乐文件地址(Fileurl)

-用户表(userinfo):

​	账号(user_id)

​	密码(password)

​	头像文件地址(imageurl)

-歌单表(musiclistinfo):

​	歌单名(ListName)

​	用户id(user_id)

​	音乐文件地址(Fileurl)

-用户-歌单表(user_musiclist_info):

​	歌单名(ListName)

​	用户id(user_id)

##### 模块设计：

界面类：前端界面实现、用户交互设计

实体类：存储信息的基类、数据库接口类

#### 开发平台

Microsoft Visual Studio

#### 语言

C#

#### 数据库

MySQL -version:8.0.19


