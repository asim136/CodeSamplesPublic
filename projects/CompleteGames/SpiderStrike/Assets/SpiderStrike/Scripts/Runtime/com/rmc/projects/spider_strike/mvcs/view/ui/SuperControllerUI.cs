/**
 * Copyright (C) 2005-2014 by Rivello Multimedia Consulting (RMC).                    
 * code [at] RivelloMultimediaConsulting [dot] com                                                  
 *                                                                      
 * Permission is hereby granted, free of charge, to any person obtaining
 * a copy of this software and associated documentation files (the      
 * "Software"), to deal in the Software without restriction, including  
 * without limitation the rights to use, copy, modify, merge, publish,  
 * distribute, sublicense, and#or sell copies of the Software, and to   
 * permit persons to whom the Software is furnished to do so, subject to
 * the following conditions:                                            
 *                                                                      
 * The above copyright notice and this permission notice shall be       
 * included in all copies or substantial portions of the Software.      
 *                                                                      
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,      
 * EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF   
 * MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
 * IN NO EVENT SHALL THE AUTHORS BE LIABLE FOR ANY CLAIM, DAMAGES OR    
 * OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
 * ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
 * OTHER DEALINGS IN THE SOFTWARE.                                      
 */
// Marks the right margin of code *******************************************************************


//--------------------------------------
//  Imports
//--------------------------------------
using UnityEngine;
using strange.extensions.mediation.impl;
using com.rmc.projects.spider_strike.mvcs.controller.signals;

//--------------------------------------
//  Namespace
//--------------------------------------
using com.rmc.projects.spider_strike.mvcs.model.vo;
using com.rmc.projects.spider_strike.mvcs.view.signals;
using System.Collections.Generic;
using System.Linq;


namespace com.rmc.projects.spider_strike.mvcs.view.ui
{
	
	//--------------------------------------
	//  Namespace Properties
	//--------------------------------------
	
	
	//--------------------------------------
	//  Class Attributes
	//--------------------------------------
	
	//--------------------------------------
	//  Class
	//--------------------------------------
	public class SuperControllerUI : View 
	{
		
		//--------------------------------------
		//  Properties
		//--------------------------------------
		
		// GETTER / SETTER
		
		// PUBLIC
		
		/// <summary>
		/// The user interface button clicked signal.
		/// </summary>
		public UIInputChangedSignal uiInputChangedSignal {set; get;}

		
		// PUBLIC STATIC
		
		// PRIVATE
		/// <summary>
		/// The _last user interface input V os_list.
		/// </summary>
		public List<UIInputVO> _lastUIInputVOs_list;

		/// <summary>
		/// Sets the visibility.
		/// </summary>
		private bool _isVisible_boolean;
		public bool isVisible
		{ 
			get{
				return _isVisible_boolean;
			}
			set
			{
				_isVisible_boolean = value;
			}
		}

		
		// PRIVATE STATIC
		
		//--------------------------------------
		//  Methods
		//--------------------------------------		
		///<summary>
		///	Use this for initialization
		///</summary>
		override protected void Start () 
		{
			base.Start();
			_lastUIInputVOs_list = new List<UIInputVO>();
			_lastUIInputVOs_list.Add (new UIInputVO (KeyCode.LeftArrow, UIInputEventType.DownExit));
			_lastUIInputVOs_list.Add (new UIInputVO (KeyCode.RightArrow, UIInputEventType.DownExit));
			

		
		}


		/// <summary>
		/// Update this instance.
		/// </summary>
		void Update ()
		{
			_doProcessDownStayEvents();

		}
		
		
		// PUBLIC
		/// <summary>
		/// Init this instance.
		/// </summary>
		virtual public void init()
		{
			uiInputChangedSignal = new UIInputChangedSignal ();
			
		}
		
		/// <summary>
		/// Raises the destroy event.
		/// </summary>
		override protected void OnDestroy ()
		{
			//
			base.OnDestroy();
			
			//
			uiInputChangedSignal = null; //overkill to existing garbage-collection.
			
		}
		
		
		// PUBLIC STATIC
		
		// PRIVATE
		/// <summary>
		/// _dos the update user interface input.
		/// 
		/// NOTE: We store the state of the last UIInputVO **PER** keycode. This way we can dispatch
		/// 		if/when the 'DownStay' should be sent every frame.
		/// 
		/// 
		/// 
		/// </summary>
		/// <param name="aKeyCode">A key code.</param>
		/// <param name="aUIInputEventType">A user interface input event type.</param>
		protected void _doUpdateUIInput (KeyCode aKeyCode, UIInputEventType aUIInputEventType )
		{
			UIInputVO lastFoundUIInputVO 	= _lastUIInputVOs_list.Where(uiInputVO => uiInputVO.keyCode == aKeyCode).SingleOrDefault();
			UIInputVO toSendUIInputVO 		= new UIInputVO (aKeyCode, aUIInputEventType);


			_lastUIInputVOs_list.Remove (lastFoundUIInputVO);
			//lastFoundUIInputVO.uiInputEventType = aUIInputEventType;
			//_lastUIInputVOs_list.Add (lastFoundUIInputVO);
			//Debug.Log ("after" + _lastUIInputVOs_list.Count);


			//1. CREATE/STORE THAT INPUTVO
			if (true) {
				//Debug.Log ("ADD: " + _lastUIInputVOs_list.Count);
			} else {
				//2. OR UPDATE THE INPUTVO
				Debug.Log ("update ("+aKeyCode+"): " + aUIInputEventType);

				//SENDS #1, DOWN AND #2, UP
				//_lastUIInputVOs_list.Remove (lastFoundUIInputVO);
				//lastFoundUIInputVO.uiInputEventType = aUIInputEventType;
				//_lastUIInputVOs_list.Add (lastFoundUIInputVO);
			}
			Debug.Log ("SEND: " + toSendUIInputVO);
			uiInputChangedSignal.Dispatch (toSendUIInputVO);
			
		}

		/// <summary>
		/// _dos the process down stay events.
		/// 
		/// NOTE: We use the state information here to repeatedly send 'yes I'm currently down' events
		/// 
		/// 
		/// 
		/// </summary>
		private void _doProcessDownStayEvents ()
		{

			//Debug.Log ("--");
			foreach (UIInputVO uiInputVO in _lastUIInputVOs_list) {
				//Debug.Log ("check ("+uiInputVO.keyCode+") =" + uiInputVO.uiInputEventType);
				if (uiInputVO.uiInputEventType == UIInputEventType.DownEnter) {

					//SENDS #3, STAY
					uiInputChangedSignal.Dispatch (new UIInputVO (uiInputVO.keyCode, UIInputEventType.DownStay));
				}

			}
		}

		
		// PRIVATE STATIC
		
		// PRIVATE COROUTINE
		
		// PRIVATE INVOKE
		
		//--------------------------------------
		//  Events 
		//		(This is a loose term for -- handling incoming messaging)
		//
		//--------------------------------------
	}
}

