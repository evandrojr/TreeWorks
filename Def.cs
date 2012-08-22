#region Using directives

using System;
using System.Collections.Generic;
using System.Text;

#endregion

namespace Spartacus
{
    public class Def
    {
		public const string APPLICATION_NAME = "TreeWorks";
		public const string PROGRAMMER_NAME = "Evandro M Leite Junior";
		public const string PROGRAMMER_EMAIL = "jr@evandro.org";
		public const string SUPERVISOR = "Paul R Harper";
		public const string INSTITUTION = "Southampton University";
		public const string IMAGE_DIR = @"\img";
		public static string APPLICATION_DIR = System.Environment.CurrentDirectory;
//        public static Db.DriverEnum DatabaseDriver;
        public static string DataSourceFileNameWithPath;
        public static string DataSourceFileName;
        public static string DataSourceFileBaseName;
        public static string DataSourceFilePath;
        public static string DataSourceExcelSheet = "Sheet1";
        public static FrmMain FrmMain;
        public static System.Windows.Forms.PictureBox PbBase;
        public static System.Windows.Forms.Panel PanelMain;
        public static System.Windows.Forms.ToolStrip ToolBar;
        public static TreeValidation TreeValidation;
        public static bool Multivariate = false;

        public static Schema Schema;
        public static Tree Tree;
        public static Database Db;
        public static int DatasetWithNullRowCount; //ALL ROWS
        public static int DatasetNotNullRowCount; //WHERE DEP VARIABLE IS NOT NULL
        public static int TrainingSetRowCount;
        public static int TrainingSetPercent;
        public static int TestingSetRowCount;
        public static int TestingSetPercent;
        public static DataImport DataImportDefs;
        public static string LogMessage = "";
        public static bool ExperimentRunning = false;

#region Layout definitions (Lt)
        public static int LtSizeDifferencePanelMainAndPbBase = 3;
        public static bool TreeCanBeDisplayed {
            get {
                if (ExperimentRunning)
                    return false;
                return Def.FrmMain.TreeCanBeDisplayed;   
            }
            set {
                if(!Def.ExperimentRunning)
                    Def.FrmMain.TreeCanBeDisplayed = value;
            }
        }
#endregion

#region General Options

        public static int DefaultPercOfDataUsedForLearningSample = 70;
        public static int TreeLevelsMax = 10;
        public static int NodeDataViewRowsMax = 500;
        public static bool SampleUsingTheSameSeed = true;

#endregion General Options

#region Database Options

        public static string DbDriver = "PostgreSQL ANSI";
        //public static string DbDriver = "PostgreSQL UTF8";
        //public static string DbDriver = "PostgreSQL";
        public static string DbUser = "postgres";
        public static string DbPassword = "tornado";
        public static string DbDatabase = "postgres";
        public static string DbServer = "localhost";
//        public static string DbServer = "192.168.8.5";
        public static string DbPort = "5432";

        public static string DbTableIdName = "id_spartacus";
        public static string DbBaseTableSufix = "_bs";
        public static string DbBaseTableMvSufix = "_mvbs";
        public static string DbTrainingTableSufix = "_tr";
        public static string DbTrainingTableMvSufix = "_mvtr";
        public static string DbTestTableSufix = "_ts";
        public static string DbTestTableMvSufix = "_mvts";
//        public static string DbReferenceTableSufix = "";
        public static string DbNumericFormat = "float4";
        public static string DbTextFormat = "varchar(50)";

        public static string DbTableInUse = "";

        public static string DbTrTb {
            get { return DbTableInUse + DbTrainingTableSufix; }
        }

        public static string DbBsTb {
            get { return DbTableInUse + DbBaseTableSufix; }
        }

        public static string DbBsMvTb {
            get { return DbTableInUse + DbBaseTableMvSufix; }
        }

        public static string DbTsTb {
            get { return DbTableInUse + DbTestTableSufix; }
        }

        public static string DbTsMvTb {
            get { return DbTableInUse + DbTestTableMvSufix; }
        }

#endregion Database Options

#region Decision Tree Options

        public static int TreeMinNumberOfCasesPerNode = 1;
        public static int ClfMaxNumberOfValuesForFullSearch = 16;
        public static int ClfOptimisationLevelForCatSearch = 1;
        public static int PartitionCountMax = 4095;
        public static int MaxDifferentCatValues = PartitionCountMax + 2;

#endregion Decision Tree Options

#region Data import options

        public static bool ImportedDataRemoveRowsWithBlanks = false;

#endregion Data import options


#region Optmisations

public static bool ProgressiveGainCatValSearchEnhanced = true;

#endregion Optmisations


#region Minimum Sample size Options

public static double SampleSizeCI = 90;
        public static double SampleSizeError = 5;

#endregion Minimum Sample size Options


#region Multivariate options

        //public static double PresentCoefficientValue = 0.5; //Used for coding the categories into numbers
        //public static double AbsentCoefficientValue = -0.5;

        public static double PresentCoefficientValue = 0.5; //Used for coding the categories into numbers
        public static double AbsentCoefficientValue = 0;


#endregion Multivariate options

        static Def()
		{
        }

    }
}
