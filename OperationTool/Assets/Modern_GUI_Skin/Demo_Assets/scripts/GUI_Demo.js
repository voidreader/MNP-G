#pragma strict

var guiSkin: GUISkin;


private var windowRect : Rect = Rect (0, 0, 400, 380);
private var toggleTxt : boolean = false;
private var stringToEdit : String = "Text Label";	
private	var textToEdit : String = "TextBox:\nHello World\nI've got few lines...";
private var hSliderValue : float = 0.0;
private var vSliderValue : float = 0.0;
private var hSbarValue : float;
private var vSbarValue : float;
private var scrollPosition : Vector2 = Vector2.zero;


function Start () 
{
  windowRect.x = (Screen.width - windowRect.width)/2;
  windowRect.y = (Screen.height - windowRect.height)/2;
}


function OnGUI () 
{
  GUI.skin = guiSkin;
  
   windowRect = GUI.Window (0, windowRect, DoMyWindow, "My Window");
  
  
}
	
	

function DoMyWindow (windowID : int) 
{

    GUI.Box(Rect(10,50,120,250),"Box title");
    GUI.Button (Rect (20,80,100,20), "BUTTON");
    GUI.Label (Rect (20, 115, 100, 20), "LABEL: Hello!");
    stringToEdit = GUI.TextField (Rect (15, 140, 110, 20), stringToEdit, 25);
    hSliderValue = GUI.HorizontalSlider (Rect (15, 175, 110, 30), hSliderValue, 0.0, 10.0);
    
    vSliderValue = GUI.VerticalSlider (Rect (140, 50, 20, 200), vSliderValue, 100.0, 0.0);
    
    
    toggleTxt = GUI.Toggle(Rect(165, 50, 100, 30), toggleTxt, "A Toggle text");
    textToEdit = GUI.TextArea (Rect (165, 90, 185, 100), textToEdit, 200);
    
   GUI.Label (Rect (180, 215, 100, 20), "ScrollView");
    scrollPosition = GUI.BeginScrollView (Rect (180,235,160,100), scrollPosition, Rect (0, 0, 220, 200));
		GUI.Button (Rect (0,10,100,20), "Top-left");
		GUI.Button (Rect (120,10,100,20), "Top-right");
		GUI.Button (Rect (0,170,100,20), "Bottom-left");
		GUI.Button (Rect (120,170,100,20), "Bottom-right");
	GUI.EndScrollView ();
		
		
    hSbarValue = GUI.HorizontalScrollbar (Rect (10, 360, 360, 30), hSbarValue, 5.0, 0.0, 10.0);
    vSbarValue = GUI.VerticalScrollbar(Rect (380, 25, 30, 300), vSbarValue, 1.0, 30.0, 0.0);
    
			
	GUI.DragWindow (Rect (0,0,10000,10000));
}