
using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace ImageScrollView
{
	public partial class ImageScrollViewController : UIViewController
	{
		#region Constructors

		// The IntPtr and initWithCoder constructors are required for items that need 
		// to be able to be created from a xib rather than from managed code

		public ImageScrollViewController (IntPtr handle) : base(handle)
		{
			Initialize ();
		}

		[Export("initWithCoder:")]
		public ImageScrollViewController (NSCoder coder) : base(coder)
		{
			Initialize ();
		}

		public ImageScrollViewController () : base("ImageScrollViewController", null)
		{
			Initialize ();
		}

		void Initialize ()
		{
		}
		
		#endregion
		
		UIScrollView scrollView;
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			scrollView = new UIScrollView (new RectangleF (0, 0, 320, 480));
			this.View.AddSubview (scrollView);
			//scrollView.Delegate = new ViewControllerForDuplicateEndCapsDelegate ();
			
			
			// add the last image (image4) into the first position
			this.AddImageWithName ("Images/image4.jpg", 0);
			
			// add all of the images to the scroll view
			for (int i = 1; i < 5; i++) {
				this.AddImageWithName (string.Format ("Images/image{0}.jpg", i), i);
			}
			
			// add the first image (image1) into the last position
			this.AddImageWithName ("Images/image1.jpg", 5);
			
			scrollView.PagingEnabled = true;
			scrollView.Bounces = true;
			scrollView.DelaysContentTouches = true;
			scrollView.ShowsHorizontalScrollIndicator = false;
			
			scrollView.ContentSize = new System.Drawing.SizeF (1920, 480);
			scrollView.ScrollRectToVisible (new RectangleF (320, 0, 320, 480), true);
			scrollView.DecelerationEnded += HandleDecelerationEnded;
			
		}

		void HandleDecelerationEnded (object sender, EventArgs e)
		{
			// The key is repositioning without animation      
			if (scrollView.ContentOffset.X == 0) 
			{         
				// user is scrolling to the left from image 1 to image 4         
				// reposition offset to show image 4 that is on the right in the scroll view         
				scrollView.ScrollRectToVisible(new RectangleF(1280, 0, 320, 480), false);
			}    
			else if (scrollView.ContentOffset.X == 1600) 
			{         
				// user is scrolling to the right from image 4 to image 1        
				// reposition offset to show image 1 that is on the left in the scroll view         
				scrollView.ScrollRectToVisible(new RectangleF(320, 0, 320, 480), false);
			} 	
		}

		void AddImageWithName (string imageString, int position)
		{
			// add image to scroll view
			UIImage image = UIImage.FromFile (imageString);
			UIImageView imageView = new UIImageView (image);
			
			imageView.Frame = new System.Drawing.RectangleF (position * 320, 0, 320, 480);
			
			scrollView.AddSubview (imageView);
		}
	}
}

