<big>**一 项目介绍**</big>

　　本项目大致分为10个部分： 基本人物控制与人物动画、普通继电器的拾取与放置、高空落地后溅起灰尘、 场景切换、 电流机制、完成全部7个关卡的基本功能、电流振动的特效、场景转换动画、陷阱路破碎时的碎片效果、细腻顺滑的人物操作手感。

<big>**二 各个部分的实现思路**</big>

① 基本人物控制与人物动画：给人物添加rigidbody2D、boxcollider2D等组件，通过监测键盘输入来改变rigidbody2D的速度。并在人物移动过程中采用Physics2D.OverlapBoxAll( )来监测人物上下左右是否有其他物体或人物阻挡，防止player撞动另一个player或继电器等物体。人物动画则使用unity制作，并使用animator controller控制动画播放。

② 普通继电器的拾取与放置：采用Physics2D.OverlapBoxAll( )来监测人物附近是否有普通继电器，并通过监测键盘输入来控制拾取，拾取继电器时，将继电器的rigidbody2D组件删除、令boxcollider2D组件失效，并将继电器设为player下的一个子物体，令其跟随player移动。放置的实现与拾取相反，为继电器添加rigidbody2D组件、恢复boxcollider2D组件，并取消继电器的父子关系。

③　高空落地后溅起灰尘：首先使用Unity制作灰尘动画，并将其保存为Prefab，在物体第一次落地时生成该Prefab即可。

④ 场景切换：场景切换包含通关后进入下一关和死亡后重新开始当前关卡，使用scenemanager进行场景切换，通关后进入下一关使用 SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1)，死亡后重新开始当前关卡使用SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex)。

⑤ 电流机制：将场景中的player和继电器的Transform加入到一个List中，并从这些点中找到从一个player到另一个player的最短路径。点与点的距离超过3，则将距离视为无穷大。

⑥ 电流振动的特效：使用LineRenderer，并通过向LineRenderer添加position来实现电流震动特效。

⑦ 场景转换动画：在UI中利用一张白色图片的旋转来制作场景转换动画。

⑧ 陷阱路破碎时的碎片效果：参考https://blog.csdn.net/qq_36926782/article/details/109013041

⑨ 细腻顺滑的人物操作手感：对人物的横向速度，跳跃初始速度，垂直速度的衰减系数等数值进行调整。

<big>**三 一些技术亮点**</big>

① 地图制作：使用Tilemap绘制地图非常方便，以及Grid组件很容易对物体的位置进行网格控制。

② 场景转换动画：利用UI制作动画特别巧妙。

③ 陷阱路破碎时的碎片效果：利用随机生成切割点以及Sprite.Create(Texture texture, Rect rect, Vector2 vector)，对原图像进行切割，并对生成的物体添加rigidbody2D、boxcollider2D等组件，再对其施加随机的爆炸力，让新生成的物体Layer设置为“Fragment”，并且在Project Settings->Physics2D->Layer Collision Matrix中取消“Fragment”与其他Layer的碰撞（防止生成的碎片干扰玩家移动）。

④ 电流振动的特效：利用随机插值的方法产生了电流振动的特效。
