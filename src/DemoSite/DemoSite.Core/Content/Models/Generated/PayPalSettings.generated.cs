//------------------------------------------------------------------------------
// <auto-generated>
//   This code was generated by a tool.
//
//    Umbraco.ModelsBuilder.Embedded v11.2.0+cf1cd51
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
	/// <summary>PayPal Settings</summary>
	[PublishedModel("payPalSettings")]
	public partial class PayPalSettings : PublishedContentModel, IPaymentMethodSettings
	{
		// helpers
#pragma warning disable 0109 // new is redundant
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "11.2.0+cf1cd51")]
		public new const string ModelTypeAlias = "payPalSettings";
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "11.2.0+cf1cd51")]
		public new const PublishedItemType ModelItemType = PublishedItemType.Content;
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "11.2.0+cf1cd51")]
		[return: global::System.Diagnostics.CodeAnalysis.MaybeNull]
		public new static IPublishedContentType GetModelContentType(IPublishedSnapshotAccessor publishedSnapshotAccessor)
			=> PublishedModelUtility.GetModelContentType(publishedSnapshotAccessor, ModelItemType, ModelTypeAlias);
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "11.2.0+cf1cd51")]
		[return: global::System.Diagnostics.CodeAnalysis.MaybeNull]
		public static IPublishedPropertyType GetModelPropertyType<TValue>(IPublishedSnapshotAccessor publishedSnapshotAccessor, Expression<Func<PayPalSettings, TValue>> selector)
			=> PublishedModelUtility.GetModelPropertyType(GetModelContentType(publishedSnapshotAccessor), selector);
#pragma warning restore 0109

		private IPublishedValueFallback _publishedValueFallback;

		// ctor
		public PayPalSettings(IPublishedContent content, IPublishedValueFallback publishedValueFallback)
			: base(content, publishedValueFallback)
		{
			_publishedValueFallback = publishedValueFallback;
		}

		// properties

		///<summary>
		/// Access Token
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "11.2.0+cf1cd51")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("productionAccessToken")]
		public virtual string ProductionAccessToken => this.Value<string>(_publishedValueFallback, "productionAccessToken");

		///<summary>
		/// Client ID
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "11.2.0+cf1cd51")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("productionClientId")]
		public virtual string ProductionClientId => this.Value<string>(_publishedValueFallback, "productionClientId");

		///<summary>
		/// Access Token
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "11.2.0+cf1cd51")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("stagingAccessToken")]
		public virtual string StagingAccessToken => this.Value<string>(_publishedValueFallback, "stagingAccessToken");

		///<summary>
		/// Client ID
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "11.2.0+cf1cd51")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("stagingClientId")]
		public virtual string StagingClientId => this.Value<string>(_publishedValueFallback, "stagingClientId");

		///<summary>
		/// Restrict Collection Days To
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "11.2.0+cf1cd51")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("restrictCollectionDaysTo")]
		public virtual global::System.Collections.Generic.List<global::N3O.Umbraco.Lookups.DayOfMonth> RestrictCollectionDaysTo => global::DemoSite.Content.PaymentMethodSettings.GetRestrictCollectionDaysTo(this, _publishedValueFallback);

		///<summary>
		/// Transaction Description
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "11.2.0+cf1cd51")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("transactionDescription")]
		public virtual string TransactionDescription => global::DemoSite.Content.PaymentMethodSettings.GetTransactionDescription(this, _publishedValueFallback);

		///<summary>
		/// Transaction ID
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "11.2.0+cf1cd51")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("transactionId")]
		public virtual string TransactionId => global::DemoSite.Content.PaymentMethodSettings.GetTransactionId(this, _publishedValueFallback);
	}
}
