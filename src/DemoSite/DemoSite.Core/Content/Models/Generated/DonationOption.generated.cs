//------------------------------------------------------------------------------
// <auto-generated>
//   This code was generated by a tool.
//
//    Umbraco.ModelsBuilder.Embedded v10.2.1+25a20cf
//
//   Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Linq.Expressions;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.PublishedCache;
using Umbraco.Cms.Infrastructure.ModelsBuilder;
using Umbraco.Cms.Core;
using Umbraco.Extensions;

namespace DemoSite.Content
{
	// Mixin Content Type with alias "donationOption"
	/// <summary>Donation Option</summary>
	public partial interface IDonationOption : IPublishedContent
	{
		/// <summary>Default Giving Type</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.2.1+25a20cf")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		global::N3O.Umbraco.Giving.Lookups.GivingType DefaultGivingType { get; }

		/// <summary>Location</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.2.1+25a20cf")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		global::DemoSite.Content.FundDimension1Value Dimension1 { get; }

		/// <summary>Theme</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.2.1+25a20cf")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		global::DemoSite.Content.FundDimension2Value Dimension2 { get; }

		/// <summary>Stipulation</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.2.1+25a20cf")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		global::DemoSite.Content.FundDimension3Value Dimension3 { get; }

		/// <summary>Hide</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.2.1+25a20cf")]
		bool HideDonation { get; }

		/// <summary>Hide Quantity</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.2.1+25a20cf")]
		bool HideQuantity { get; }

		/// <summary>Hide</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.2.1+25a20cf")]
		bool HideRegularGiving { get; }
	}

	/// <summary>Donation Option</summary>
	[PublishedModel("donationOption")]
	public partial class DonationOption : PublishedContentModel, IDonationOption
	{
		// helpers
#pragma warning disable 0109 // new is redundant
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.2.1+25a20cf")]
		public new const string ModelTypeAlias = "donationOption";
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.2.1+25a20cf")]
		public new const PublishedItemType ModelItemType = PublishedItemType.Content;
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.2.1+25a20cf")]
		[return: global::System.Diagnostics.CodeAnalysis.MaybeNull]
		public new static IPublishedContentType GetModelContentType(IPublishedSnapshotAccessor publishedSnapshotAccessor)
			=> PublishedModelUtility.GetModelContentType(publishedSnapshotAccessor, ModelItemType, ModelTypeAlias);
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.2.1+25a20cf")]
		[return: global::System.Diagnostics.CodeAnalysis.MaybeNull]
		public static IPublishedPropertyType GetModelPropertyType<TValue>(IPublishedSnapshotAccessor publishedSnapshotAccessor, Expression<Func<DonationOption, TValue>> selector)
			=> PublishedModelUtility.GetModelPropertyType(GetModelContentType(publishedSnapshotAccessor), selector);
#pragma warning restore 0109

		private IPublishedValueFallback _publishedValueFallback;

		// ctor
		public DonationOption(IPublishedContent content, IPublishedValueFallback publishedValueFallback)
			: base(content, publishedValueFallback)
		{
			_publishedValueFallback = publishedValueFallback;
		}

		// properties

		///<summary>
		/// Default Giving Type
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.2.1+25a20cf")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("defaultGivingType")]
		public virtual global::N3O.Umbraco.Giving.Lookups.GivingType DefaultGivingType => GetDefaultGivingType(this, _publishedValueFallback);

		/// <summary>Static getter for Default Giving Type</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.2.1+25a20cf")]
		[return: global::System.Diagnostics.CodeAnalysis.MaybeNull]
		public static global::N3O.Umbraco.Giving.Lookups.GivingType GetDefaultGivingType(IDonationOption that, IPublishedValueFallback publishedValueFallback) => that.Value<global::N3O.Umbraco.Giving.Lookups.GivingType>(publishedValueFallback, "defaultGivingType");

		///<summary>
		/// Location
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.2.1+25a20cf")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("dimension1")]
		public virtual global::DemoSite.Content.FundDimension1Value Dimension1 => GetDimension1(this, _publishedValueFallback);

		/// <summary>Static getter for Location</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.2.1+25a20cf")]
		[return: global::System.Diagnostics.CodeAnalysis.MaybeNull]
		public static global::DemoSite.Content.FundDimension1Value GetDimension1(IDonationOption that, IPublishedValueFallback publishedValueFallback) => that.Value<global::DemoSite.Content.FundDimension1Value>(publishedValueFallback, "dimension1");

		///<summary>
		/// Theme
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.2.1+25a20cf")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("dimension2")]
		public virtual global::DemoSite.Content.FundDimension2Value Dimension2 => GetDimension2(this, _publishedValueFallback);

		/// <summary>Static getter for Theme</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.2.1+25a20cf")]
		[return: global::System.Diagnostics.CodeAnalysis.MaybeNull]
		public static global::DemoSite.Content.FundDimension2Value GetDimension2(IDonationOption that, IPublishedValueFallback publishedValueFallback) => that.Value<global::DemoSite.Content.FundDimension2Value>(publishedValueFallback, "dimension2");

		///<summary>
		/// Stipulation
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.2.1+25a20cf")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("dimension3")]
		public virtual global::DemoSite.Content.FundDimension3Value Dimension3 => GetDimension3(this, _publishedValueFallback);

		/// <summary>Static getter for Stipulation</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.2.1+25a20cf")]
		[return: global::System.Diagnostics.CodeAnalysis.MaybeNull]
		public static global::DemoSite.Content.FundDimension3Value GetDimension3(IDonationOption that, IPublishedValueFallback publishedValueFallback) => that.Value<global::DemoSite.Content.FundDimension3Value>(publishedValueFallback, "dimension3");

		///<summary>
		/// Hide
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.2.1+25a20cf")]
		[ImplementPropertyType("hideDonation")]
		public virtual bool HideDonation => GetHideDonation(this, _publishedValueFallback);

		/// <summary>Static getter for Hide</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.2.1+25a20cf")]
		public static bool GetHideDonation(IDonationOption that, IPublishedValueFallback publishedValueFallback) => that.Value<bool>(publishedValueFallback, "hideDonation");

		///<summary>
		/// Hide Quantity
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.2.1+25a20cf")]
		[ImplementPropertyType("hideQuantity")]
		public virtual bool HideQuantity => GetHideQuantity(this, _publishedValueFallback);

		/// <summary>Static getter for Hide Quantity</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.2.1+25a20cf")]
		public static bool GetHideQuantity(IDonationOption that, IPublishedValueFallback publishedValueFallback) => that.Value<bool>(publishedValueFallback, "hideQuantity");

		///<summary>
		/// Hide
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.2.1+25a20cf")]
		[ImplementPropertyType("hideRegularGiving")]
		public virtual bool HideRegularGiving => GetHideRegularGiving(this, _publishedValueFallback);

		/// <summary>Static getter for Hide</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.2.1+25a20cf")]
		public static bool GetHideRegularGiving(IDonationOption that, IPublishedValueFallback publishedValueFallback) => that.Value<bool>(publishedValueFallback, "hideRegularGiving");
	}
}
