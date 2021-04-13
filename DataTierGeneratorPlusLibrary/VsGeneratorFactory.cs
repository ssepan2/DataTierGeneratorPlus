using System;
using DataTierGeneratorPlusLibrary.MVC;

namespace DataTierGeneratorPlusLibrary
{
	/// <summary>
	/// VsGeneratorFactory creates the appropriate VS.Net language generator.
	/// </summary>
	internal class VsGeneratorFactory
	{
		public VsGeneratorFactory()
		{
		}
        //TODO:use model--SJS
		internal static IVsGenerator GetGenerator(GeneratorModel model)
		{
			IVsGenerator objGenerator = null;

			// (Optionally) build the utility class
            switch (model.SQLHelper)
			{
                case GeneratorModel.SQLHelper_BuiltIn:
				    //Generate internal library
                    switch (model.Language)
                    {
                        case GeneratorModel.Language_CSharp:
                            objGenerator = new CsGeneratorBuiltIn();
                            break;
                        case GeneratorModel.Language_VisualBasic:
                            objGenerator = new VbGeneratorBuiltIn();
                            break;
                        // LANGUAGE CUSTOMIZATION: 3a of 4 -- add additional case here for additional language class (using built-in library)
                        //case GeneratorModel.Language_JSharp:
                        //    objGenerator = new JsGeneratorBuiltIn();
                        //    break;
                        //case GeneratorModel.Language_CPlusPlus:
                        //    objGenerator = new CpGeneratorBuiltIn();
                        //    break;
                        //case GeneratorModel.Language_Xxxx:
                        //	objGenerator = new XxGeneratorBuiltIn();
                        //	break;
                        default:
                            {
                                throw new IndexOutOfRangeException();
                            }
                    }
                    break;
                //case GeneratorModel.SQLHelper_EnterpriseLibrary:
                //    //we are building against Enterprise helper library; 
                //    switch (model.Language)
                //    {
                //        //case GeneratorModel.Language_CSharp:
                //        //    objGenerator = new CsGeneratorEnterpriseLibrary();
                //        //    break;
                //       // case GeneratorModel.Language_VisualBasic:
                //       //     objGenerator = new VbGeneratorEnterpriseLibrary();
                //       //     break;
                //        // LANGUAGE CUSTOMIZATION: 3b of 4 -- add additional case here for additional language class (using enterprise library)
                //        //case GeneratorModel.Language_JSharp:
                //        //    objGenerator = new JsGeneratorEnterpriseLibrary();
                //        //    break;
                //        //case GeneratorModel.Language_CPlusPlus:
                //        //    objGenerator = new CpGeneratorEnterpriseLibrary();
                //        //    break;
                //        //case GeneratorModel.Language_Xxxx:
                //        //	objGenerator = new XxGeneratorEnterpriseLibrary();
                //        //	break;
                //        default:
                //            {
                //                throw new IndexOutOfRangeException();
                //            }
                //    }
                //    break;
                //case GeneratorModel.SQLHelper_DotNetNuke:
                //    //we are building against DotNetNuke helper library; 
                //    switch (model.Language)
                //    {
                //        case GeneratorModel.Language_CSharp:
                //            //objGenerator = new CsGeneratorDotNetNuke();
                //            throw new IndexOutOfRangeException();
                //            break;
                //        case GeneratorModel.Language_VisualBasic:
                //            //objGenerator = new VbGeneratorDotNetNuke();
                //            throw new IndexOutOfRangeException();
                //            break;
                //        // LANGUAGE CUSTOMIZATION: 3c of 4 -- add additional case here for additional language class (using dotnetnuke)
                //        //case GeneratorModel.Language_JSharp:
                //        //    // objGenerator = new JsGeneratorDotNetNuke();
                //        //    throw new IndexOutOfRangeException();
                //        //    break;
                //        //case GeneratorModel.Language_CPlusPlus:
                //        //    //objGenerator = new CpGeneratorDotNetNuke();
                //        //    throw new IndexOutOfRangeException();
                //        //    break; 
                //        //case GeneratorModel.Language_Xxxx:
                //        //	objGenerator = new XxGeneratorDotNetNuke();
                //        //	break;
                //        default:
                //            {
                //                throw new IndexOutOfRangeException();
                //            }
                //    }
                //    break;
		        default:
		        {
			        throw new IndexOutOfRangeException();
		        }
			} 

			return objGenerator;
		}
	}
}
