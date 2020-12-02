using System;

namespace DataTierGeneratorPlus
{
	/// <summary>
	/// VsGeneratorFactory creates the appropriate VS.Net language generator.
	/// </summary>
	internal class VsGeneratorFactory
	{
		public VsGeneratorFactory()
		{
		}

		internal static IVsGenerator GetGenerator(Settings settings)
		{
			IVsGenerator objGenerator = null;

			// (Optionally) build the utility class
            switch (settings.SQLHelper)
			{
                case Settings.SQLHelper_BuiltIn:
				    //Generate internal library
                    switch (settings.Language)
                    {
                        case Settings.Language_CSharp:
                            objGenerator = new CsGeneratorBuiltIn();
                            break;
                        case Settings.Language_VisualBasic:
                            objGenerator = new VbGeneratorBuiltIn();
                            break;
                        // LANGUAGE CUSTOMIZATION: 3a of 4 -- add additional case here for additional language class (using built-in library)
                        //case Settings.Language_JSharp:
                        //    objGenerator = new JsGeneratorBuiltIn();
                        //    break;
                        //case Settings.Language_CPlusPlus:
                        //    objGenerator = new CpGeneratorBuiltIn();
                        //    break;
                        //case Settings.Language_Xxxx:
                        //	objGenerator = new XxGeneratorBuiltIn();
                        //	break;
                        default:
                            {
                                throw new IndexOutOfRangeException();
                            }
                    }
                    break;
                //case Settings.SQLHelper_EnterpriseLibrary:
                //    //we are building against Enterprise helper library; 
                //    switch (settings.Language)
                //    {
                //        //case Settings.Language_CSharp:
                //        //    objGenerator = new CsGeneratorEnterpriseLibrary();
                //        //    break;
                //       // case Settings.Language_VisualBasic:
                //       //     objGenerator = new VbGeneratorEnterpriseLibrary();
                //       //     break;
                //        // LANGUAGE CUSTOMIZATION: 3b of 4 -- add additional case here for additional language class (using enterprise library)
                //        //case Settings.Language_JSharp:
                //        //    objGenerator = new JsGeneratorEnterpriseLibrary();
                //        //    break;
                //        //case Settings.Language_CPlusPlus:
                //        //    objGenerator = new CpGeneratorEnterpriseLibrary();
                //        //    break;
                //        //case Settings.Language_Xxxx:
                //        //	objGenerator = new XxGeneratorEnterpriseLibrary();
                //        //	break;
                //        default:
                //            {
                //                throw new IndexOutOfRangeException();
                //            }
                //    }
                //    break;
                //case Settings.SQLHelper_DotNetNuke:
                //    //we are building against DotNetNuke helper library; 
                //    switch (settings.Language)
                //    {
                //        case Settings.Language_CSharp:
                //            //objGenerator = new CsGeneratorDotNetNuke();
                //            throw new IndexOutOfRangeException();
                //            break;
                //        case Settings.Language_VisualBasic:
                //            //objGenerator = new VbGeneratorDotNetNuke();
                //            throw new IndexOutOfRangeException();
                //            break;
                //        // LANGUAGE CUSTOMIZATION: 3c of 4 -- add additional case here for additional language class (using dotnetnuke)
                //        //case Settings.Language_JSharp:
                //        //    // objGenerator = new JsGeneratorDotNetNuke();
                //        //    throw new IndexOutOfRangeException();
                //        //    break;
                //        //case Settings.Language_CPlusPlus:
                //        //    //objGenerator = new CpGeneratorDotNetNuke();
                //        //    throw new IndexOutOfRangeException();
                //        //    break; 
                //        //case Settings.Language_Xxxx:
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
