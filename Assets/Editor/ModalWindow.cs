using UnityEngine;
 using UnityEditor;
 using System.Collections;
 using System.Collections.Generic;
 
 public class ModalWindow
 {
     private static int m_WinIDManager = 1337;
     protected int m_WinID = m_WinIDManager++;
     protected bool enabled = true;
 
     public event System.Action<ModalWindow> OnWindow;
     public string title;
     public Rect position;
     public bool ShouldClose = false;    
     public int ID { get { return m_WinID; } }
     public ModalWindow(Rect aInitialPos, string aTitle)
     {
         position = aInitialPos;
         title = aTitle;
     }
     public ModalWindow(Rect aInitialPos, string aTitle, System.Action<ModalWindow> aCallback)
     {
         position = aInitialPos;
         title = aTitle;
         OnWindow += aCallback;
     }
     public virtual void OnGUI()
     {
         // For some strange reason Unity only disables the window border but not the content
         // so we save the enabled state and use it inside the window callback down below.
         enabled = GUI.enabled;
         position = GUI.Window(m_WinID, position, DrawWindow, title);
     }
 
     protected virtual void DrawWindow(int id)
     {
         // restore the enabled state
         GUI.enabled = enabled;
         if (OnWindow != null)
             OnWindow(this);
     }
     public virtual void Close()
     {
         ShouldClose = true;
     }
 }
 
 public class ModalSystem
 {
     private List<ModalWindow> m_List = new List<ModalWindow>();
     public bool IsWindowOpen { get { return m_List.Count > 0; } }
     public ModalWindow Top
     {
         get { return IsWindowOpen ? m_List[m_List.Count - 1] : null; }
     }
     public void Draw()
     {
         // remove closed windows
         if (Event.current.type == EventType.Layout)
         {
             for (int i = m_List.Count - 1; i >= 0; i--)
                 if (m_List[i].ShouldClose)
                     m_List.RemoveAt(i);
         }
         if (m_List.Count > 0)
         {
             // draw all windows
             for (int i = 0; i < m_List.Count; i++)
             {
                 GUI.enabled = (i == m_List.Count - 1); // disable all except the last
                 GUI.BringWindowToFront(m_List[i].ID); // order them from back to front
                 GUI.FocusWindow(m_List[i].ID);       //               ||
                 m_List[i].OnGUI();
             }
         }
     }
     public void Add(ModalWindow aWindow)
     {
         m_List.Add(aWindow);
     }
 }
 
 public class EditorWindowTest : EditorWindow
 {
     [MenuItem("Tools/TestWindow")]
     public static void Init()
     {
         GetWindow<EditorWindowTest>();
     }
 
     ModalSystem modalWindows = new ModalSystem();
 
     void OpenPopup(string aTitle)
     {
         var win = new ModalWindow(new Rect(30, 30, position.width - 60, position.height - 60), aTitle, (w) =>
         {
             if (GUILayout.Button("Open another popup"))
                 OpenPopup(aTitle + "->subwindow");
             if (GUILayout.Button("Close"))
                 w.Close();
             GUI.DragWindow();
         });
         modalWindows.Add(win);
     }
 
     void OnGUI()
     {
         GUI.enabled = !modalWindows.IsWindowOpen;
         if (GUILayout.Button("Open Popup"))
         {
             OpenPopup("First");
         }
         if (GUILayout.Button("Some other GUI stuff"))
         {
             Debug.Log("Doing stuff...");
         }
         BeginWindows();
         modalWindows.Draw();
         EndWindows();
     }
 }