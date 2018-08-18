  // Do learn to insert your names and a brief description of
  // what the program is supposed to do!

  // This is a skeleton program for developing a parser for C declarations
  // P.D. Terry, Rhodes University, 2015

  using Library;
  using System;
  using System.Text;

  class Token {
    public int kind;
    public string val;

    public Token(int kind, string val) {
      this.kind = kind;
      this.val = val;
    } // constructor

  } // Token

  class Declarations {

    // +++++++++++++++++++++++++ File Handling and Error handlers ++++++++++++++++++++

    static InFile input;
    static OutFile output;

    static string NewFileName(string oldFileName, string ext) {
    // Creates new file name by changing extension of oldFileName to ext
      int i = oldFileName.LastIndexOf('.');
      if (i < 0) return oldFileName + ext; else return oldFileName.Substring(0, i) + ext;
    } // NewFileName

    static void ReportError(string errorMessage) {
    // Displays errorMessage on standard output and on reflected output
      Console.WriteLine(errorMessage);
      output.WriteLine(errorMessage);
    } // ReportError

    static void Abort(string errorMessage) {
    // Abandons parsing after issuing error message
      ReportError(errorMessage);
      output.Close();
      System.Environment.Exit(1);
    } // Abort

    // +++++++++++++++++++++++  token kinds enumeration +++++++++++++++++++++++++

    const int
      noSym = 0,
      EOFSym = 1,
      intSym = 2,
      charSym = 3,
      boolSym = 4,
      voidSym = 5,
      numSym = 6,
      identSym = 7,
      lparenSym = 8,
      rparenSym = 9,
      lbrackSym = 10,
      rbrackSym = 11,
      pointerSym = 12,
      commaSym = 13,
      semicolonSym = 14;

    string[] symbols = new string[11] { "int", "char", "bool", "void", "(", ")", "{", "}", ",", ";", "*" };
      // and others like this

    // +++++++++++++++++++++++++++++ Character Handler ++++++++++++++++++++++++++

    const char EOF = '\0';
    static bool atEndOfFile = false;

    // Declaring ch as a global variable is done for expediency - global variables
    // are not always a good thing

    static char ch;    // look ahead character for scanner

    static void GetChar() {
    // Obtains next character ch from input, or CHR(0) if EOF reached
    // Reflect ch to output
      if (atEndOfFile) ch = EOF;
      else {
        ch = input.ReadChar();
        atEndOfFile = ch == EOF;
        //if (!atEndOfFile) output.Write(ch);
      }
    } // GetChar

    // +++++++++++++++++++++++++++++++ Scanner ++++++++++++++++++++++++++++++++++

    // Declaring sym as a global variable is done for expediency - global variables
    // are not always a good thing

    static Token sym;

    static void GetSym() {
    // Scans for next sym from input
      while (ch > EOF && ch <= ' ') GetChar();
      StringBuilder symLex = new StringBuilder();
      int symKind = noSym;
      while (ch != ' ') 
      {
          symLex.Append(ch);
          GetChar();
      }
      symKind  = CheckSym(symLex.ToString());  
      
        // over to you!

      sym = new Token(symKind, symLex.ToString());
    } // GetSym

    static bool IsNumber(string str)
    {
        foreach(char c in str)
        {
            if (!Char.IsDigit(c)) return false;
        }

        return true;
    }
    static int CheckSym(string symbl)
    {

        if(IsNumber(symbl)) return numSym;

        switch (symbl)
        {
            case "int": return intSym;
            case "char": return charSym;
            case "bool": return boolSym;
            case "void": return voidSym;
            case "("   : return lparenSym;
            case ")"   : return rparenSym;
            case "["   : return lbrackSym;
            case "]"   : return rbrackSym;
            case "*"   : return pointerSym;    
            default: return identSym;

        }

    }
        /*  ++++ Commented out for the moment

          // +++++++++++++++++++++++++++++++ Parser +++++++++++++++++++++++++++++++++++

          static void Accept(int wantedSym, string errorMessage) {
          // Checks that lookahead token is wantedSym
            if (sym.kind == wantedSym) GetSym(); else Abort(errorMessage);
          } // Accept

          static void Accept(IntSet allowedSet, string errorMessage) {
          // Checks that lookahead token is in allowedSet
            if (allowedSet.Contains(sym.kind)) GetSym(); else Abort(errorMessage);
          } // Accept

          static void CDecls() {}

        ++++++ */

        // +++++++++++++++++++++ Main driver function +++++++++++++++++++++++++++++++

     public static void Main(string[] args) {
      // Open input and output files from command line arguments
      if (args.Length == 0) {
        Console.WriteLine("Usage: Declarations FileName");
        System.Environment.Exit(1);
      }
      input = new InFile(args[0]);
      output = new OutFile(NewFileName(args[0], ".out"));

      GetChar();                                  // Lookahead character

  //  To test the scanner we can use a loop like the following:

      do {
        GetSym();                                 // Lookahead symbol
        OutFile.StdOut.Write(sym.kind, 3);
        OutFile.StdOut.WriteLine(" " + sym.val);  // See what we got
      } while (sym.kind != EOFSym);

  /*  After the scanner is debugged we shall substitute this code:

      GetSym();                                   // Lookahead symbol
      CDecls();                                   // Start to parse from the goal symbol
      // if we get back here everything must have been satisfactory
      Console.WriteLine("Parsed correctly");

  */
      output.Close();
    } // Main

  } // Declarations

