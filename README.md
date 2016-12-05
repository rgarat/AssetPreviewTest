# AssetPreviewTest
Test Project for testing overriding AssetPreview.GetAssetPreview()

This project is trying to help solve how to get a custom texture from AssetPreview.GetAssetPreview(). 
The behavior we want to get is similarto how prefabs with sprites in them work. When you select the prefab in the project window 
with the zoom slider to the right you see a preview of the sprites, and AssetPreview.GetAssetPreview provides that same txture.

The project consists of two components "CustomObjectComponent" and "AnotherCustomObjectComponent", these components have a reference to a sprite
and I want to create some editor tooling to help show that sprite as the preview.

I am going to try two methods 

* using the ObjectPreview class and the CustomPreview attribute with CustomObjectComponent 
* using a custom inspector editor and RenderStaticPreview for AnotherCustomObjectComponent

So far neither of those methods seem to be working, with the ObjectPreview class, I am able to get a similar behavior as with sprites 
in the inspector, when I select the prefab or a gameobject instance with a CustomObjectComponent monobehavior in there, the whole object
gets the sprite as the preview.

But with the RenderStaticPreview method I am not getting any result, I am not seeing logs nor am able to breakpoint, I am not sure if its the wrong solution
or I am doing any mistake.

in the Prefabs folder, there are 3 prefabs, one using each method, and another one that just has a child sprite, to show how it should behave.


There is also a custom editor window, in Tools/ShowPreviewPalette that shows the previews for those prefabs, this code is similar to the real 
use I want to achieve to make a palette for a custom level editor.
