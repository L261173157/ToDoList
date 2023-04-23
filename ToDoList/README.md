# ToDoList

桌面便签

1. 桌面便签，学习用，需要增加app.config，配置里面的数据库连接字符串及密钥
2. 用了Prism，wpf的mvvm框架
3. view层用了一些HandyControl里面的控件
4. Db连接数据库用的EF，用的是CodeFirst，数据库用的是mysql
5. **[感谢JetBrains提供的开源许可证](https://jb.gg/OpenSource)**
6. 本地词典用的https://github.com/skywind3000/ECDICT ，非常感谢！

当前阶段已知bug：  
1.下面输入框偶尔会自动退出，目前未锁定问题  
2.翻译模块，当本地数据库无法查询，会进行web查询，但是未返回结果时，再次输入查询，会把上次结果直接输出  
