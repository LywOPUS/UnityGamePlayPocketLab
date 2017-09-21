# ReadMe
- vrSDK的使用
- 对象池的使用
- 动态烘培场景导航
- 向量的计算 
- 物理的模拟
- 关于在vr下的渲染问题
- 性能的优化

#参考资料

http://www.myzaker.com/article/58820e441bc8e0aa47000015/
http://www.jianshu.com/p/33ef625885dc


#The Lab Renderer的使用


> The Lab Renderer使用Forward Rendering的原因主要是为了MSAA(MultiSampling Anti-Aliasing)和效率。然而Unity默认的Forward Rendering使用了Multi-Pass来渲染所有灯光（每个物体的每个动态灯要多一个Pass来渲染它的光照），The Lab Renderer提供了一个单Pass渲染多个灯光的解决方案。 

> 官方文档翻译 http://www.manew.com/thread-90270-1-1.html


- 在Player Settings中打开Forward Rendering
- 把Color Space改成Linear(Unity就是一直赖在Gamma空间)
- 把ValveCamera.cs加到主相机上
- 把ValveRealtimeLight.cs加到所有实时的灯光上
- 设置Shadow Cascades的分级成No Cascades
- 设置Pixel Light Count成99

#关于Draw Call 和Batch的区别

参考：http://blog.csdn.net/hany3000/article/details/44033243