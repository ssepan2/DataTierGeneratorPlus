using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using Ssepan.Utility;

namespace DataTierGeneratorPlusLibrary 
{
	internal sealed class Utility 
	{
		private static String invalidFileCharacters = @"\/+|?<>*:";

		private Utility() 
		{
		}
		
		/// <summary>
		/// Creates the specified sub-directory, if it doesn't exist.
		/// </summary>
		/// <param name="name">The name of the sub-directory to be created.</param>
		internal static void CreateSubDirectory
        (
            String name
        ) 
		{
            try
            {
                if (!Directory.Exists(name))
                {
                    Directory.CreateDirectory(name);
                }

            }
            catch (Exception ex)
            {
                Log.Write(ex, MethodBase.GetCurrentMethod(), EventLogEntryType.Error);
                Log.Write(
                    Log.FormatEntry(String.Format("Directory name: {0}", name), MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name),
                    EventLogEntryType.Error);
                throw;
            }
        }
		
		/// <summary>
		/// Creates the specified sub-directory, if it doesn't exist.
		/// </summary>
		/// <param name="name">The name of the sub-directory to be created.</param>
		/// <param name="deleteIfExists">Indicates if the directory should be deleted if it exists.</param>
		internal static void CreateSubDirectory
        (
            String name, 
            Boolean deleteIfExists
        ) 
		{
            try
            {
                if (Directory.Exists(name))
                {
                    if (deleteIfExists)
                    {
                        Directory.Delete(name, true);
                        Directory.CreateDirectory(name);
                    }
                }
                else
                {
                    Directory.CreateDirectory(name);
                }

            }
            catch (Exception ex)
            {
                Log.Write(ex, MethodBase.GetCurrentMethod(), EventLogEntryType.Error);
                Log.Write(
                    Log.FormatEntry(String.Format("Directory name: {0}", name), MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name),
                    EventLogEntryType.Error);
                throw;
            }
        }
		
		/// <summary>
		/// Retrieves the specified manifest resource stream from the executing assembly as a string.
		/// </summary>
		/// <param name="name">Name of the resource to retrieve.</param>
		/// <returns>The value of the specified manifest resource.</returns>
		internal static String GetResource
        (
            String name
        ) 
		{
            String returnValue = default(String);
            try
            {
                using (StreamReader streamReader = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(name)))
                {
                    returnValue = streamReader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex, MethodBase.GetCurrentMethod(), EventLogEntryType.Error);
                Log.Write(
                    Log.FormatEntry(String.Format("Resource name: {0}", name), MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name),
                    EventLogEntryType.Error);
                throw;
            }
            return returnValue;
        }
		
		/// <summary>
		/// Retrieves the specified manifest resource stream from the executing assembly as a string, replacing the specified old value with the specified new value.
		/// </summary>
		/// <param name="name">Name of the resource to retrieve.</param>
		/// <param name="oldValue">A String to be replaced.</param>
		/// <param name="newValue">A String to replace all occurrences of oldValue.</param>
		/// <returns>The value of the specified manifest resource, with all instances of oldValue replaced with newValue.</returns>
		internal static String GetResource
        (
            String name, 
            String oldValue, 
            String newValue
        ) 
		{
            String returnValue = default(String);
            try
            {
                returnValue = GetResource(name);
                returnValue = returnValue.Replace(oldValue, newValue);
            }
            catch (Exception ex)
            {
                Log.Write(ex, MethodBase.GetCurrentMethod(), EventLogEntryType.Error);
                throw;
            }
            return returnValue;
        }

		/// <summary>
		/// Formats a String in Camel case (the first letter is in lower case).
		/// </summary>
		/// <param name="original">A String to be formatted.</param>
		/// <returns>A String in Camel case.</returns>
		internal static String FormatCamel
        (
            String original
        ) 
		{
            String returnValue = default(String);
            try
            {
                if (original.Length > 0)
                {
                    returnValue = original.Substring(0, 1).ToLower() + original.Substring(1);
                }
                else
                {
                    returnValue = String.Empty;
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex, MethodBase.GetCurrentMethod(), EventLogEntryType.Error);
                Log.Write(
                    Log.FormatEntry(String.Format("Original string: {0}", original), MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name),
                    EventLogEntryType.Error);
                throw;
            }
            return returnValue;
        }

		/// <summary>
		/// Formats a String in Pascal case (the first letter is in upper case).
		/// </summary>
		/// <param name="original">A String to be formatted.</param>
		/// <returns>A String in Pascal case.</returns>
		internal static String FormatPascal
        (
            String original
        ) 
		{
            String returnValue = default(String);
            try
            {
                if (original.Length > 0)
                {
                    returnValue = original.Substring(0, 1).ToUpper() + original.Substring(1);
                }
                else
                {
                    returnValue = String.Empty;
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex, MethodBase.GetCurrentMethod(), EventLogEntryType.Error);
                Log.Write(
                    Log.FormatEntry(String.Format("Original string: {0}", original), MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name),
                    EventLogEntryType.Error);
                throw;
            }
            return returnValue;
        }

		/// <summary>
		/// Cleans whitespace inside a string.
		/// </summary>
		/// <param name="original">A String to be cleaned.</param>
		/// <returns>A cleaned string.</returns>
		internal static String CleanWhitespace
        (
            String original
        )
		{
            String returnValue = default(String);
            try
            {
                if (original.Length > 0)
                {
                    returnValue = Regex.Replace(original, @"[\s]", "");
                }
                else
                {
                    returnValue = String.Empty;
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex, MethodBase.GetCurrentMethod(), EventLogEntryType.Error);
                Log.Write(
                    Log.FormatEntry(String.Format("Original string: {0}", original), MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name),
                    EventLogEntryType.Error);
                throw;
            }
            return returnValue;
        }

		internal static String FilterPathCharacters
        (
            String unfilteredFilePath
        )
		{
			String tempFilePath;
            String returnValue = default(String);

            try
            {
                tempFilePath = unfilteredFilePath;
                foreach (char invalidPathChar in invalidFileCharacters) //Path.InvalidPathChars )
                {
                    tempFilePath = tempFilePath.Replace(invalidPathChar, '_');
                }
                returnValue = tempFilePath;
            }
            catch (Exception ex)
            {
                Log.Write(ex, MethodBase.GetCurrentMethod(), EventLogEntryType.Error);
                Log.Write(
                    Log.FormatEntry(String.Format("Unfiltered File Path: {0}", unfilteredFilePath), MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name),
                    EventLogEntryType.Error);
                throw;
            }
            return returnValue;
        }
	}
}
