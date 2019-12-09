// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:3.0.0.0
//      SpecFlow Generator Version:3.0.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace Menes.Specs.Features
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.0.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("OpenApiAccessCheckerExtensions")]
    [NUnit.Framework.CategoryAttribute("perScenarioContainer")]
    public partial class OpenApiAccessCheckerExtensionsFeature
    {
        
        private TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "OpenApiAccessCheckerExtensions.feature"
#line hidden
        
        [NUnit.Framework.OneTimeSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "OpenApiAccessCheckerExtensions", "\tIn order to ensure that I only return links to a user if they have permission to" +
                    " use them\r\n\tAs a developer\r\n\tI want to be able to check all links in a HalDocume" +
                    "nt based response in a single call", ProgrammingLanguage.CSharp, new string[] {
                        "perScenarioContainer"});
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [NUnit.Framework.OneTimeTearDownAttribute()]
        public virtual void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        [NUnit.Framework.SetUpAttribute()]
        public virtual void TestInitialize()
        {
        }
        
        [NUnit.Framework.TearDownAttribute()]
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioInitialize(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioInitialize(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<NUnit.Framework.TestContext>(NUnit.Framework.TestContext.CurrentContext);
        }
        
        public virtual void ScenarioStart()
        {
            testRunner.OnScenarioStart();
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        public virtual void FeatureBackground()
        {
#line 7
#line 8
 testRunner.Given("I have a HalDocument called \'embeddedDocument\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
            TechTalk.SpecFlow.Table table2 = new TechTalk.SpecFlow.Table(new string[] {
                        "Rel",
                        "OperationId",
                        "Href",
                        "OperationType"});
            table2.AddRow(new string[] {
                        "self",
                        "getEmbedded",
                        "/things/target/embedded/1",
                        "get"});
            table2.AddRow(new string[] {
                        "itemInEmbeddedDocumentToWhichUserDoesNotHavePermissionButIsEmbedded",
                        "getEmbeddedLink",
                        "/things/target/embedded/1/1",
                        "get"});
            table2.AddRow(new string[] {
                        "itemInEmbeddedDocumentToWhichUserDoesNotHavePermission",
                        "getEmbeddedLink",
                        "/things/target/embedded/1/2",
                        "get"});
#line 9
 testRunner.And("the HalDocument called \'embeddedDocument\' has internal links", ((string)(null)), table2, "And ");
#line 14
 testRunner.And("I have a HalDocument called \'embeddedDocument2\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            TechTalk.SpecFlow.Table table3 = new TechTalk.SpecFlow.Table(new string[] {
                        "Rel",
                        "OperationId",
                        "Href",
                        "OperationType"});
            table3.AddRow(new string[] {
                        "self",
                        "getEmbedded",
                        "/things/target/embedded/2",
                        "get"});
#line 15
 testRunner.And("the HalDocument called \'embeddedDocument2\' has internal links", ((string)(null)), table3, "And ");
#line 18
 testRunner.And("I have a HalDocument called \'target\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            TechTalk.SpecFlow.Table table4 = new TechTalk.SpecFlow.Table(new string[] {
                        "Rel",
                        "OperationId",
                        "Href",
                        "OperationType"});
            table4.AddRow(new string[] {
                        "self",
                        "getTarget",
                        "/things/target",
                        "get"});
            table4.AddRow(new string[] {
                        "itemInTargetToWhichUserHasPermission",
                        "getEmbedded",
                        "/things/target/embedded/1",
                        "get"});
            table4.AddRow(new string[] {
                        "itemInTargetToWhichUserDoesNotHavePermissionButIsEmbedded",
                        "getEmbedded",
                        "/things/target/embedded/2",
                        "get"});
#line 19
 testRunner.And("the HalDocument called \'target\' has internal links", ((string)(null)), table4, "And ");
#line hidden
            TechTalk.SpecFlow.Table table5 = new TechTalk.SpecFlow.Table(new string[] {
                        "Rel",
                        "Object"});
            table5.AddRow(new string[] {
                        "itemInTargetToWhichUserHasPermission",
                        "{embeddedDocument}"});
            table5.AddRow(new string[] {
                        "itemInTargetToWhichUserDoesNotHavePermissionButIsEmbedded",
                        "{embeddedDocument2}"});
#line 24
 testRunner.And("the HalDocument called \'target\' has embedded resources", ((string)(null)), table5, "And ");
#line hidden
            TechTalk.SpecFlow.Table table6 = new TechTalk.SpecFlow.Table(new string[] {
                        "Url",
                        "OperationType"});
            table6.AddRow(new string[] {
                        "/things/target/embedded/1/1",
                        "get"});
            table6.AddRow(new string[] {
                        "/things/target/embedded/1/2",
                        "get"});
            table6.AddRow(new string[] {
                        "/things/target/embedded/2",
                        "get"});
#line 28
 testRunner.And("the current user does not have permission to", ((string)(null)), table6, "And ");
#line hidden
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Default checking is recursive; child item links to which the user doesn\'t have ac" +
            "cess are removed")]
        [NUnit.Framework.CategoryAttribute("useChildObjects")]
        public virtual void DefaultCheckingIsRecursiveChildItemLinksToWhichTheUserDoesntHaveAccessAreRemoved()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Default checking is recursive; child item links to which the user doesn\'t have ac" +
                    "cess are removed", null, new string[] {
                        "useChildObjects"});
#line 35
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line 7
this.FeatureBackground();
#line hidden
            TechTalk.SpecFlow.Table table7 = new TechTalk.SpecFlow.Table(new string[] {
                        "Option"});
            table7.AddRow(new string[] {
                        "None"});
#line 36
 testRunner.When("I ask the access checker to remove forbidden links from the HalDocument called \'t" +
                    "arget\' with the following options", ((string)(null)), table7, "When ");
#line hidden
            TechTalk.SpecFlow.Table table8 = new TechTalk.SpecFlow.Table(new string[] {
                        "Rel"});
            table8.AddRow(new string[] {
                        "self"});
            table8.AddRow(new string[] {
                        "itemInTargetToWhichUserHasPermission"});
#line 39
 testRunner.Then("the HalDocument called \'target\' should contain only the following link relations", ((string)(null)), table8, "Then ");
#line hidden
            TechTalk.SpecFlow.Table table9 = new TechTalk.SpecFlow.Table(new string[] {
                        "Rel"});
            table9.AddRow(new string[] {
                        "self"});
#line 43
 testRunner.And("the HalDocument called \'embeddedDocument\' should contain only the following link " +
                    "relations", ((string)(null)), table9, "And ");
#line hidden
            TechTalk.SpecFlow.Table table10 = new TechTalk.SpecFlow.Table(new string[] {
                        "Rel"});
            table10.AddRow(new string[] {
                        "itemInTargetToWhichUserHasPermission"});
#line 46
 testRunner.And("the HalDocument called \'target\' should contain only the following embedded resour" +
                    "ces", ((string)(null)), table10, "And ");
#line 49
 testRunner.And("the HalDocument called \'embeddedDocument\' should contain no embedded resources", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("When non-recursive checking is used, only links and resources from the top level " +
            "document are removed")]
        [NUnit.Framework.CategoryAttribute("useChildObjects")]
        public virtual void WhenNon_RecursiveCheckingIsUsedOnlyLinksAndResourcesFromTheTopLevelDocumentAreRemoved()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("When non-recursive checking is used, only links and resources from the top level " +
                    "document are removed", null, new string[] {
                        "useChildObjects"});
#line 52
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line 7
this.FeatureBackground();
#line hidden
            TechTalk.SpecFlow.Table table11 = new TechTalk.SpecFlow.Table(new string[] {
                        "Option"});
            table11.AddRow(new string[] {
                        "NonRecursive"});
#line 53
 testRunner.When("I ask the access checker to remove forbidden links from the HalDocument called \'t" +
                    "arget\' with the following options", ((string)(null)), table11, "When ");
#line hidden
            TechTalk.SpecFlow.Table table12 = new TechTalk.SpecFlow.Table(new string[] {
                        "Rel"});
            table12.AddRow(new string[] {
                        "self"});
            table12.AddRow(new string[] {
                        "itemInTargetToWhichUserHasPermission"});
#line 56
 testRunner.Then("the HalDocument called \'target\' should contain only the following link relations", ((string)(null)), table12, "Then ");
#line hidden
            TechTalk.SpecFlow.Table table13 = new TechTalk.SpecFlow.Table(new string[] {
                        "Rel"});
            table13.AddRow(new string[] {
                        "self"});
            table13.AddRow(new string[] {
                        "itemInEmbeddedDocumentToWhichUserDoesNotHavePermissionButIsEmbedded"});
            table13.AddRow(new string[] {
                        "itemInEmbeddedDocumentToWhichUserDoesNotHavePermission"});
#line 60
 testRunner.And("the HalDocument called \'embeddedDocument\' should contain only the following link " +
                    "relations", ((string)(null)), table13, "And ");
#line hidden
            TechTalk.SpecFlow.Table table14 = new TechTalk.SpecFlow.Table(new string[] {
                        "Rel"});
            table14.AddRow(new string[] {
                        "itemInTargetToWhichUserHasPermission"});
#line 65
 testRunner.And("the HalDocument called \'target\' should contain only the following embedded resour" +
                    "ces", ((string)(null)), table14, "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("When unsafe checking is used, only links that do not have corresponding embedded " +
            "resources are removed.")]
        [NUnit.Framework.CategoryAttribute("useChildObjects")]
        public virtual void WhenUnsafeCheckingIsUsedOnlyLinksThatDoNotHaveCorrespondingEmbeddedResourcesAreRemoved_()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("When unsafe checking is used, only links that do not have corresponding embedded " +
                    "resources are removed.", null, new string[] {
                        "useChildObjects"});
#line 70
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line 7
this.FeatureBackground();
#line hidden
            TechTalk.SpecFlow.Table table15 = new TechTalk.SpecFlow.Table(new string[] {
                        "Option"});
            table15.AddRow(new string[] {
                        "Unsafe"});
#line 71
 testRunner.When("I ask the access checker to remove forbidden links from the HalDocument called \'t" +
                    "arget\' with the following options", ((string)(null)), table15, "When ");
#line hidden
            TechTalk.SpecFlow.Table table16 = new TechTalk.SpecFlow.Table(new string[] {
                        "Rel"});
            table16.AddRow(new string[] {
                        "self"});
            table16.AddRow(new string[] {
                        "itemInTargetToWhichUserHasPermission"});
            table16.AddRow(new string[] {
                        "itemInTargetToWhichUserDoesNotHavePermissionButIsEmbedded"});
#line 74
 testRunner.Then("the HalDocument called \'target\' should contain only the following link relations", ((string)(null)), table16, "Then ");
#line hidden
            TechTalk.SpecFlow.Table table17 = new TechTalk.SpecFlow.Table(new string[] {
                        "Rel"});
            table17.AddRow(new string[] {
                        "self"});
#line 79
 testRunner.And("the HalDocument called \'embeddedDocument\' should contain only the following link " +
                    "relations", ((string)(null)), table17, "And ");
#line hidden
            TechTalk.SpecFlow.Table table18 = new TechTalk.SpecFlow.Table(new string[] {
                        "Rel"});
            table18.AddRow(new string[] {
                        "itemInTargetToWhichUserHasPermission"});
            table18.AddRow(new string[] {
                        "itemInTargetToWhichUserDoesNotHavePermissionButIsEmbedded"});
#line 82
 testRunner.And("the HalDocument called \'target\' should contain only the following embedded resour" +
                    "ces", ((string)(null)), table18, "And ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
