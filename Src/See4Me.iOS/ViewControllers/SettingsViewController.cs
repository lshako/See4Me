﻿using System;
using System.Drawing;
using AVFoundation;
using Foundation;
using GalaSoft.MvvmLight.Messaging;
using See4Me.iOS.Common;
using See4Me.ViewModels;
using UIKit;
using System.Collections.Generic;
using GalaSoft.MvvmLight.Helpers;
using See4Me.iOS.Extensions;
using See4Me.Localization.Resources;
using See4Me.Services;
using CoreGraphics;

namespace See4Me
{
	public partial class SettingsViewController : ViewControllerBase<SettingsViewModel>
	{
		private List<Binding> bindings;

		public SettingsViewController(IntPtr handle) : base(handle)
		{ }

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			ViewModel.Initialize();

			this.RegisterMessages();
			this.SetTranslation();
			this.bindings = new List<Binding>()
			{
				this.SetBinding(() => ViewModel.VisionSubscriptionKey, () => TextVisionSubscriptionKey.Text, BindingMode.TwoWay),
				this.SetBinding(() => ViewModel.EmotionSubscriptionKey, () => TextEmotionSubscriptionKey.Text, BindingMode.TwoWay),
				this.SetBinding(() => ViewModel.TranslatorSubscriptionKey, () => TextTranslatorSubscriptionKey.Text, BindingMode.TwoWay),

				this.SetBinding(() => ViewModel.IsTextToSpeechEnabled, () => SwitchTextToSpeech.On, BindingMode.TwoWay),
				this.SetBinding(() => ViewModel.ShowRecognitionConfidence, () => SwitchShowDescriptionConfidence.On, BindingMode.TwoWay),
				this.SetBinding(() => ViewModel.ShowOriginalDescriptionOnTranslation, () => SwitchShowOriginalDescription.On, BindingMode.TwoWay),
			};

			TextVisionSubscriptionKey.DismissKeyboardOnReturn();
			TextEmotionSubscriptionKey.DismissKeyboardOnReturn();
			TextTranslatorSubscriptionKey.DismissKeyboardOnReturn();

			ButtonAbout.SetCommand(this.ViewModel.GotoAboutCommand);
			ButtonPrivacy.SetCommand(this.ViewModel.GotoPrivacyPolicyCommand);

			this.NavigationItem.SetRightBarButtonItem(
				new UIBarButtonItem(UIBarButtonSystemItem.Save, (sender, args) =>
				{
					this.ViewModel.Save();
				})
			, true);

			//dismiss the keyboard if the user taps anywhere in the view
			var g = new UITapGestureRecognizer(() => View.EndEditing(true));
			g.CancelsTouchesInView = false; //for iOS5
			View.AddGestureRecognizer(g);

			NavigationController.SetNavigationBarHidden(false, false);
		}

		public override void ViewWillDisappear(bool animated)
		{
			base.ViewWillDisappear(animated);

			NavigationController.SetNavigationBarHidden(true, false);
		}

		private void RegisterMessages()
		{

		}

		private void SetTranslation()
		{
			this.NavigationItem.Title = AppResources.Settings;

			LabelVisionSubscriptionKey.Text = AppResources.VisionSubscriptionKey;
			LabelEmotionSubscriptionKey.Text = AppResources.EmotionSubscriptionKey;
			LabelTranslatorSubscriptionKey.Text = AppResources.TranslatorSubscriptionKey;

			LabelTextToSpeech.Text = AppResources.TextToSpeech;
			LabelShowDescriptionConfidence.Text = AppResources.ShowDescriptionConfidence;
			LabelShowOriginalDescription.Text = AppResources.ShowOriginalDescriptionOnTranslation;

			ButtonAbout.SetTitle(AppResources.AboutCommand, UIControlState.Normal);
			ButtonPrivacy.SetTitle(AppResources.PrivacyPolicyCommand, UIControlState.Normal);
		}
	}
}

