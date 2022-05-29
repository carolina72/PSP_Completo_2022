using System;
using System.Security.Permissions;
using System.Security.Principal;
using System.Threading;

class GenericIdentityMembers
{
    public static void DemonstrateWindowsBuiltInRoleEnum()
    {
        AppDomain myDomain = Thread.GetDomain();
        myDomain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal);
        WindowsPrincipal myPrincipal = (WindowsPrincipal)Thread.CurrentPrincipal;
        Console.WriteLine("{0} belongs to: ", myPrincipal.Identity.Name.ToString());
        Array wbirFields = Enum.GetValues(typeof(WindowsBuiltInRole));
        foreach (object roleName in wbirFields)
        {
            try
            {
                // Cast the role name to a RID represented by the WindowsBuildInRole value.
                Console.WriteLine("{0}? {1}.", roleName,
                    myPrincipal.IsInRole((WindowsBuiltInRole)roleName));
                Console.WriteLine("The RID for this role is: " + ((int)roleName).ToString());

            }
            catch (Exception)
            {
                Console.WriteLine("{0}: Could not obtain role for this RID.",
                    roleName);
            }
        }
        // Get the role using the string value of the role.
        Console.WriteLine("{0}? {1}.", "Administrators",
            myPrincipal.IsInRole("BUILTIN\\" + "Administrators"));
        Console.WriteLine("{0}? {1}.", "Users",
            myPrincipal.IsInRole("BUILTIN\\" + "Users"));
        // Get the role using the WindowsBuiltInRole enumeration value.
        Console.WriteLine("{0}? {1}.", WindowsBuiltInRole.Administrator,
           myPrincipal.IsInRole(WindowsBuiltInRole.Administrator));
        // Get the role using the WellKnownSidType.
        SecurityIdentifier sid = new SecurityIdentifier(WellKnownSidType.BuiltinAdministratorsSid, null);
        Console.WriteLine("WellKnownSidType BuiltinAdministratorsSid  {0}? {1}.", sid.Value, myPrincipal.IsInRole(sid));
    }
    [STAThread]
    static void Main(string[] args)
    {
        // Create a GenericIdentity object with no authentication type 
        // specified.
        GenericIdentity defaultIdentity = new GenericIdentity("DefaultUser");

        // Retrieve a GenericIdentity created from current WindowsIdentity
        // values.
        GenericIdentity currentIdentity = GetGenericIdentity();

        ShowIdentityPreferences(new GenericIdentity(""));
        ShowIdentityPreferences(defaultIdentity);
        ShowIdentityPreferences(currentIdentity);

        Console.WriteLine("The sample completed successfully; " +
            "press Enter to continue.");
        Console.ReadLine();
        DemonstrateWindowsBuiltInRoleEnum();
        Console.ReadLine();
        GenericIdentity myIdentity = new GenericIdentity("MyIdentity");

        // Create generic principal.
        String[] myStringArray = { "Manager", "Teller" };
        GenericPrincipal myPrincipal =
            new GenericPrincipal(myIdentity, myStringArray);

        // Attach the principal to the current thread.
        // This is not required unless repeated validation must occur,
        // other code in your application must validate, or the
        // PrincipalPermission object is used.
        Thread.CurrentPrincipal = myPrincipal;

        // Print values to the console.
        String name = myPrincipal.Identity.Name;
        bool auth = myPrincipal.Identity.IsAuthenticated;
        bool isInRole = myPrincipal.IsInRole("Manager");

        Console.WriteLine("The name is: {0}", name);
        Console.WriteLine("The isAuthenticated is: {0}", auth);
        Console.WriteLine("Is this a Manager? {0}", isInRole);
        Console.ReadLine();
        ShowIdentityPreferences(currentIdentity);
        PrincipalPermission principalPerm = new PrincipalPermission(null, "Tellerrrr");
        principalPerm.Demand();
        Console.WriteLine("Demand succeeded.");
        Console.ReadLine();
    }

    // Print identity preferences to the console window.
    private static void ShowIdentityPreferences(
        GenericIdentity genericIdentity)
    {
        // Retrieve the name of the generic identity object.
        string identityName = genericIdentity.Name;

        // Retrieve the authentication type of the generic identity object.
        string identityAuthenticationType =
            genericIdentity.AuthenticationType;

        Console.WriteLine("Name: " + identityName);
        Console.WriteLine("Type: " + identityAuthenticationType);

        // Verify that the user's identity has been authenticated
        // (was created with a valid name).
        if (genericIdentity.IsAuthenticated)
        {
            Console.WriteLine("The user's identity has been authenticated.");
        }
        else
        {
            Console.WriteLine("The user's identity has not been " +
                "authenticated.");
        }
        Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~");
    }

    // Create generic identity based on values from the current
    // WindowsIdentity.
    private static GenericIdentity GetGenericIdentity()
    {
        // Get values from the current WindowsIdentity.
        WindowsIdentity windowsIdentity = WindowsIdentity.GetCurrent();

        // Construct a GenericIdentity object based on the current Windows
        // identity name and authentication type.
        string authenticationType = windowsIdentity.AuthenticationType;
        string userName = windowsIdentity.Name;
        GenericIdentity authenticatedGenericIdentity =
            new GenericIdentity(userName, authenticationType);

        return authenticatedGenericIdentity;
    }
}
