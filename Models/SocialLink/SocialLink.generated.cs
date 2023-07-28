//--------------------------------------------------------------------------------------------------
// <auto-generated>
//
//     This code was generated by code generator tool.
//
//     To customize the code use your own partial class. For more info about how to use and customize
//     the generated code see the documentation at https://docs.xperience.io/.
//
// </auto-generated>
//--------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using CMS;
using CMS.Base;
using CMS.Helpers;
using CMS.DataEngine;
using CMS.DocumentEngine;
using CMS.DocumentEngine.Types.DancingGoatCore;

[assembly: RegisterDocumentType(SocialLink.CLASS_NAME, typeof(SocialLink))]

namespace CMS.DocumentEngine.Types.DancingGoatCore
{
	/// <summary>
	/// Represents a content item of type SocialLink.
	/// </summary>
	public partial class SocialLink : TreeNode
	{
		#region "Constants and variables"

		/// <summary>
		/// The name of the data class.
		/// </summary>
		public const string CLASS_NAME = "DancingGoatCore.SocialLink";


		/// <summary>
		/// The instance of the class that provides extended API for working with SocialLink fields.
		/// </summary>
		private readonly SocialLinkFields mFields;

		#endregion


		#region "Properties"

		/// <summary>
		/// SocialLinkID.
		/// </summary>
		[DatabaseIDField]
		public int SocialLinkID
		{
			get
			{
				return ValidationHelper.GetInteger(GetValue("SocialLinkID"), 0);
			}
			set
			{
				SetValue("SocialLinkID", value);
			}
		}


		/// <summary>
		/// SocialLinkTitle.
		/// </summary>
		[DatabaseField]
		public string SocialLinkTitle
		{
			get
			{
				return ValidationHelper.GetString(GetValue("SocialLinkTitle"), @"Follow us");
			}
			set
			{
				SetValue("SocialLinkTitle", value);
			}
		}


		/// <summary>
		/// SocialLinkUrl.
		/// </summary>
		[DatabaseField]
		public string SocialLinkUrl
		{
			get
			{
				return ValidationHelper.GetString(GetValue("SocialLinkUrl"), @"");
			}
			set
			{
				SetValue("SocialLinkUrl", value);
			}
		}


		/// <summary>
		/// Gets an object that provides extended API for working with SocialLink fields.
		/// </summary>
		[RegisterProperty]
		public SocialLinkFields Fields
		{
			get
			{
				return mFields;
			}
		}


		/// <summary>
		/// Provides extended API for working with SocialLink fields.
		/// </summary>
		[RegisterAllProperties]
		public partial class SocialLinkFields : AbstractHierarchicalObject<SocialLinkFields>
		{
			/// <summary>
			/// The content item of type SocialLink that is a target of the extended API.
			/// </summary>
			private readonly SocialLink mInstance;


			/// <summary>
			/// Initializes a new instance of the <see cref="SocialLinkFields" /> class with the specified content item of type SocialLink.
			/// </summary>
			/// <param name="instance">The content item of type SocialLink that is a target of the extended API.</param>
			public SocialLinkFields(SocialLink instance)
			{
				mInstance = instance;
			}


			/// <summary>
			/// SocialLinkID.
			/// </summary>
			public int ID
			{
				get
				{
					return mInstance.SocialLinkID;
				}
				set
				{
					mInstance.SocialLinkID = value;
				}
			}


			/// <summary>
			/// SocialLinkTitle.
			/// </summary>
			public string Title
			{
				get
				{
					return mInstance.SocialLinkTitle;
				}
				set
				{
					mInstance.SocialLinkTitle = value;
				}
			}


			/// <summary>
			/// SocialLinkUrl.
			/// </summary>
			public string Url
			{
				get
				{
					return mInstance.SocialLinkUrl;
				}
				set
				{
					mInstance.SocialLinkUrl = value;
				}
			}


			/// <summary>
			/// SocialLinkIcon.
			/// </summary>
			public IEnumerable<TreeNode> Icon
			{
				get
				{
					return (mInstance.LinkedContentItems["SocialLinkIcon"]);
				}
			}
		}

		#endregion


		#region "Constructors"

		/// <summary>
		/// Initializes a new instance of the <see cref="SocialLink" /> class.
		/// </summary>
		public SocialLink() : base(CLASS_NAME)
		{
			mFields = new SocialLinkFields(this);
		}

		#endregion
	}
}