<p align="center">
  <img width="100%" src="https://user-images.githubusercontent.com/31209389/177000374-3d491860-af2f-44ab-bdbc-6bc96f869dfb.png" /> <br>
  <a href="https://unity.com/">
	  <img width="15%" src="https://user-images.githubusercontent.com/31209389/177000465-e7ca46f1-330f-4ee0-97b1-c62c70ca3a84.png" /></a>
  <a href="https://unity.com/">
	  <img width="15%" src="https://user-images.githubusercontent.com/31209389/177000573-8430ac29-e39f-4d89-b1d4-6ba398bf7bdd.png" /></a>


</p>


<h1 align="center">
  :compass: SPICE :compass:<br>
</h1>
<p align="center">
유니티 인스팩터 커스텀 에디터 패키지<br>
Unity Inspector Custom Editor Package<br>
</p><br>
<br>

## :star: Introdution / 소개
 - **Can simply decorate the script's inspector**
 - **Readability and manipulation are intuitive**
<br>

## :package: Require / 필요
[UnityEngine](https://unity.com/)<br>
<br>

## 🧰 Use / 사용
### :open_file_folder: Package Install (Unity Asset)
- Please download it here -> **[Release](https://github.com/kibalab/SPICE_CustomEditor/releases)**
- Import it into your Unity Project Assets folder

### :page_facing_up: Editor Code Example (UdonSharp)

[**< Go to more detailed examples >**](https://github.com/kibalab/SPICE_CustomEditor/tree/master/Example/Editor/SPICE_ExampleRenderer.cs)
```CSharp
using K13A.BehaviourEditor;
[CustomEditor(typeof(CustomScript))]
public class CustomScriptEditor : Editor
{
    EditorUtil.MenuBox("Hello, MenuBox!", () =>
    {
        EditorUtil.SubMenuBox("Hello, SubMenuBox!", () =>
        {
            /* Content Here... */
        });
        /* Content Here... */
    });
}
```
### :level_slider: Example from the Inspector View
<p align="center">
  <img width="50%" src="https://user-images.githubusercontent.com/31209389/177003512-2a30b903-b5ce-465d-bf7a-28df7cb5524d.png" />
</p>
<br>

