【跨平台注意项】
* 当前引用的 Lidgren.Network , MonoGame.Framework 来自C:\Program Files (x86)\MonoGame\v3.0\Assemblies\Windows，若其他平台，需要修改引用。
* 注意项目宏定义，当前为WINDOWS和DESKTOP，若修改平台编译，需要修改宏定义。
* 注意各平台基本文件中的资源依赖

注意所有的Service命名是否标准
注意纯接口化编程，避免实体类成员暴露

* MainPlayer 和 CameraControl的整合
* 注释啊注释啊亲