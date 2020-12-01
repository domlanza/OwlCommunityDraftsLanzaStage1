using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OwlCommunityMemberLanzaDrafts
{
    public partial class frmOwlCommunity : Form
    {
        OwlMemberList thisOwlMemberList = new OwlMemberList();
        private bool addMode = false;
        private int selectedMember;
        private bool editMode = false;
        private OwlMember member;
        Validators validator = new Validators();
        public frmOwlCommunity()
        {
            InitializeComponent();
            thisOwlMemberList.addToList(new UndergraduateStudent("John", 123456789, DateTime.Today, 455, "Math", 455, 12, "Fr"));

            SFManager.writeToFile(thisOwlMemberList, FileName);
            SFManager.readFromFile(ref thisOwlMemberList, FileName);

        }

        int buttonpress = 0;
        // This index keeps track of the current Owl
        int currentIndex = -1;
        int recordsProcessedCount = 0;
        // File to read or write to
        string FileName = "PersistentObject.bin";

        // Tooltip messages
        string ttCreateUndergraduateStudent = "Click to enter Make Undergrad mode to add an Undergrad to the List of Owl Members.";
        string ttCreateGraduateStudent = "Click to enter Make GradStudent mode to add a GradStudent to the List of Owl Members.";
        string ttCreateFaculty = "Click to enter Make Faculty mode to add a Faculty to the List of Owl Members.";
        string ttCreateChairperson = "Click to enter Make ChairOwl mode to add a ChairOwl to the List of Owl Members.";
        string ttSaveUndergraduateStudent = "Click to Save the Undergrad to the List of Owl Members.";
        string ttSaveGraduateStudent = "Click to Save the GradStudent to the List of Owl Members.";
        string ttSaveChairperson = "Click to Save a Chairperson to the list of Owl Members.";
        string ttSaveFaculty = "Click to Save the Faculty to the List of Owl Members.";
        string ttClear = "Click to Clear Form.";
        string ttFind = "Click to Find a Owl in the List of Owl Members.";
        string ttDelete = "Click to Delete Owl from the List of Owl Members.";
        string ttEdit = "Click to Edit a Owl's data.";
        string ttExit = "Click to exit application.";

        string ttStudentMajor = "Enter 2 to 4 character major title - e.g., CIS, I&ST, MATH, Soc, Mus";
        string ttStudentGPA = "Enter a decimal between 0.0 and 4.0 inclusive";
        string ttUndergradTuition = "Enter dollars and cents.   No $.";
        string ttUndergradYear = "Enter mm/dd/yyyy";
        string ttUndergradCredits = "Enter an integer between 0 and 200";
        string ttGraduateStudentStipend = "Enter dollars and cents >= 0.0.  NO $";
        string ttGraduateStudentDegreeProgram = "Enter valid degree program name from table.";
        string ttOwlName = "Enter A .. Z and a .. z ONLY";
        string ttOwlBirthDate = "Enter mm/dd/yyyy";
        string ttOwlID = "Enter Exactly 9 Digits";
        string ttFacultyDepartment = "Enter department ID (two or three capital letters)";
        string ttFacultyRank = "Enter Faculty Rank as AstP, AscP, Prof, Lect, or Inst";
        string ttChairpersonStipend = "Enter dollars and cents >= 0.0.  NO $";


        private void frmOwlCommunity_Load(System.Object sender, System.EventArgs e)
        {


            // Read serialized binary data file
            SFManager.readFromFile(ref thisOwlMemberList, FileName);

            FormController.clear(this);
            FormController.activateAddButtons(this);

            // get initial Tooltips
            toolTip1.SetToolTip(btnCreateGraduateStudent, ttCreateGraduateStudent);
            toolTip1.SetToolTip(btnCreateUndergraduateStudent, ttCreateUndergraduateStudent);
            toolTip1.SetToolTip(btnCreateFaculty, ttCreateFaculty);
            toolTip1.SetToolTip(btnCreateChairperson, ttCreateChairperson);

            toolTip1.SetToolTip(btnClear, ttClear);
            toolTip1.SetToolTip(btnDelete, ttDelete);
            toolTip1.SetToolTip(btnEdit, ttEdit);
            toolTip1.SetToolTip(btnFind, ttFind);
            toolTip1.SetToolTip(btnExit, ttExit);

            toolTip1.SetToolTip(txtUndergraduateStudentTuition, ttUndergradTuition);
            toolTip1.SetToolTip(cbUndergraduateStudentYear, ttUndergradYear);
            toolTip1.SetToolTip(txtUndergraduateStudentCredits, ttUndergradCredits);
            toolTip1.SetToolTip(txtGraduateStudentStipend, ttGraduateStudentStipend);
            toolTip1.SetToolTip(cbGraduateStudentDegreeProgram, ttGraduateStudentDegreeProgram);
            toolTip1.SetToolTip(txtOwlMemberName, ttOwlName);
            toolTip1.SetToolTip(dtpOwlMemberBirthDate, ttOwlBirthDate);
            toolTip1.SetToolTip(txtOwlMemberID, ttOwlID);
            toolTip1.SetToolTip(txtStudentMajor, ttStudentMajor);
            toolTip1.SetToolTip(txtStudentMajor, ttStudentGPA);
            toolTip1.SetToolTip(txtFacultyDepartment, ttFacultyDepartment);
            toolTip1.SetToolTip(cbFacultyRank, ttFacultyRank);
            toolTip1.SetToolTip(txtChairPersonStipend, ttChairpersonStipend);
        } // end frmEmpMan_Load



        // Checks if Owl List is empty and, if not, copies the data for the
        // ith Owl to the appropriate group textboxes using the display sub.
        // Also checks to determine if the next button should be enabled.
        private void getItem(int i)
        {
            if (thisOwlMemberList.getCount() == 0)
            {
                btnDelete.Enabled = false;
                btnEdit.Enabled = false;
                // btnToString.Enabled = false;
                lblUserMessage.Text = "Please select an operation";
            }
            else if (i < 0 || i >= thisOwlMemberList.getCount())
            {
                MessageBox.Show("getItem error: index out of range");
                return;
            }
            else
            {
                currentIndex = i;
                thisOwlMemberList.getAnItem(i).Display(this);
                // thisOwlList.RemoveAt(i);
                lblUserMessage.Text = "Object Type: " + thisOwlMemberList.getAnItem(i).GetType().ToString() +
                        " List Index: " + i.ToString();
                btnFind.Enabled = true;
                btnDelete.Enabled = true;
                btnEdit.Enabled = true;
            }  // end else
        } // end getItem

        


        //Displays the part of the form for Faculty processing
        void DisplayFacultyForm()
        {
            // Display form for Create/Insert or Find/SELECT or Edit/Update or Delete a Faculty
            btnCreateFaculty.Text = "Save Faculty";
            FormController.formAddMode(this);
            txtOwlMemberID.Enabled = true;
            txtOwlMemberName.Enabled = true;
            dtpOwlMemberBirthDate.Enabled = true;
            txtFacultyDepartment.Enabled = true;
            cbFacultyRank.Enabled = true;
            btnCreateGraduateStudent.Enabled = false;
            btnCreateUndergraduateStudent.Enabled = false;
            btnCreateChairperson.Enabled = false;
            btnEnterID.Enabled = false;
            //txtChairPersonStipend.Enabled = false;
            FormController.activateFaculty(this);
            FormController.deactivateGraduateStudent(this);
            FormController.deactivateChairperson(this);
            FormController.deactivateUndergraduateStudent(this);
            toolTip1.SetToolTip(btnCreateFaculty, ttSaveFaculty);
            // txtFacultyDepartment.Enabled = true;   
            // cbFacultyRank.Enabled = true;
        } // end DisplayFacultyForm

        void DisplayChairPersonForm()
        {
            // Display form for Create/Insert or Find/SELECT or Edit/Update or Delete a Faculty
            btnCreateChairperson.Text = "Save Chairperson";
            FormController.formAddMode(this);
            txtOwlMemberID.Enabled = true;
            txtOwlMemberName.Enabled = true;
            dtpOwlMemberBirthDate.Enabled = true;
            txtFacultyDepartment.Enabled = true;
            cbFacultyRank.Enabled = true;
            btnCreateGraduateStudent.Enabled = false;
            btnCreateUndergraduateStudent.Enabled = false;
            btnCreateFaculty.Enabled = false;
            btnCreateChairperson.Enabled = true;
            txtChairPersonStipend.Enabled = true;
            btnEnterID.Enabled = false;
            FormController.activateFaculty(this);
            FormController.deactivateGraduateStudent(this);
            FormController.activateChairperson(this);
            FormController.deactivateUndergraduateStudent(this);
            toolTip1.SetToolTip(btnCreateChairperson, ttSaveChairperson);
        } // end DisplayChairpersonForm

        //Displays the part of the form for Undergrad processing
        void DisplayUndergraduateStudentForm()
        {
            // Display form for Create/Insert or Find/SELECT or Edit/Update or Delete a Undergrad
            btnCreateUndergraduateStudent.Text = "Save Undersgraduatestudent";
            FormController.formAddMode(this);
            txtOwlMemberID.Enabled = true;
            txtOwlMemberName.Enabled = true;
            dtpOwlMemberBirthDate.Enabled = true;
            txtFacultyDepartment.Enabled = false;
            cbFacultyRank.Enabled = false;
            btnCreateGraduateStudent.Enabled = false;
            btnCreateUndergraduateStudent.Enabled = true;
            btnCreateFaculty.Enabled = false;
            btnCreateChairperson.Enabled = false;
            txtChairPersonStipend.Enabled = false;
            btnEnterID.Enabled = false;
            FormController.deactivateFaculty(this);
            FormController.deactivateGraduateStudent(this);
            FormController.deactivateChairperson(this);
            FormController.activateUndergraduateStudent(this);

            toolTip1.SetToolTip(btnCreateUndergraduateStudent, ttSaveUndergraduateStudent);
        } // end DisplayFacultyForm

        //Displays the part of the form for Undergrad processing
        void DisplayGraduateStudentForm()
        {
            // Display form for Create/Insert or Find/SELECT or Edit/Update or Delete a Undergrad
            btnCreateGraduateStudent.Text = "Save GraduateStudent";
            FormController.formAddMode(this);
            txtOwlMemberID.Enabled = true;
            txtOwlMemberName.Enabled = true;
            dtpOwlMemberBirthDate.Enabled = true;
            txtFacultyDepartment.Enabled = false;
            cbFacultyRank.Enabled = false;
            btnCreateGraduateStudent.Enabled = true;
            btnCreateUndergraduateStudent.Enabled = false;
            btnCreateFaculty.Enabled = false;
            btnCreateChairperson.Enabled = false;
            txtChairPersonStipend.Enabled = false;
            btnEnterID.Enabled = false;
            FormController.deactivateFaculty(this);
            FormController.activateGraduateStudent(this);
            FormController.deactivateChairperson(this);
            FormController.deactivateUndergraduateStudent(this);
            toolTip1.SetToolTip(btnCreateGraduateStudent, ttSaveGraduateStudent);
        } // end DisplayGraduateForm

        private void btnEdit_Click(object sender, EventArgs e)
        {

            bool success;
            btnFind.Enabled = false;
            btnDelete.Enabled = false;
            btnSaveEditUpdate.Enabled = false;
            success = findAnItem("Edit/Update");
            if (success)
            {
                btnSaveEditUpdate.Enabled = true;
                btnEdit.Enabled = false;

                OwlMember p = thisOwlMemberList.getAnItem(currentIndex);
                txtOwlMemberName.Text = p.OwlName;
                txtOwlMemberID.Text = p.OwlID.ToString();
                dtpOwlMemberBirthDate.Text = Convert.ToDateTime(p.OwlBirthDate).ToString("MM/dd/yyyy");
                MessageBox.Show("Edit/UPDATE current Owl (as shown). Press Save Updates Button", "Edit/Update Notice",
                    MessageBoxButtons.OK);

                // if (thisOwlList.getAnItem(currentIndex).GetType().ToString() == "EmpMan.Undergrad")
                if (p.GetType() == typeof(UndergraduateStudent))
                {
                    FormController.activateUndergraduateStudent(this);
                    FormController.deactivateFaculty(this);
                    FormController.deactivateGraduateStudent(this);
                    FormController.deactivateChairperson(this);
                    FormController.deactivateAddButtons(this);
                    txtStudentMajor.Text = ((Student)p).getStudentMajor();
                    txtStudentGPA.Text = ((Student)p).getStudentGPA().ToString();
                    txtUndergraduateStudentTuition.Text = (((UndergraduateStudent)p).getUndergraduateStudentTuition()).ToString();
                    cbUndergraduateStudentYear.Text = (((UndergraduateStudent)p).getUndergraduateStudentYear());
                    txtUndergraduateStudentCredits.Text = (((UndergraduateStudent)p).getUndergraduateStudentCredits()).ToString();
                }
                else if (p.GetType() == typeof(GraduateStudent))
                {
                    FormController.activateGraduateStudent(this);
                    FormController.deactivateFaculty(this);
                    FormController.deactivateUndergraduateStudent(this);
                    FormController.deactivateChairperson(this);
                    FormController.deactivateAddButtons(this);
                    txtStudentMajor.Text = ((Student)p).getStudentMajor();
                    txtStudentGPA.Text = ((Student)p).getStudentGPA().ToString();
                    txtGraduateStudentStipend.Text = (((GraduateStudent)p).getGraduateStudentStipend()).ToString();
                    cbGraduateStudentDegreeProgram.Text = (((GraduateStudent)p).getGraduateStudentDegreeProgram()).ToString();
                }
                else if (p.GetType() == typeof(FacultyMember))
                {
                    FormController.activateFaculty(this);
                    FormController.deactivateGraduateStudent(this);
                    FormController.deactivateUndergraduateStudent(this);
                    FormController.deactivateChairperson(this);
                    FormController.deactivateAddButtons(this);
                    txtFacultyDepartment.Text = ((FacultyMember)p).getFacultyDepartment();
                    cbFacultyRank.Text = ((FacultyMember)p).getFacultyRank();
           
                }
                else if (p.GetType() == typeof(FacultyChairPerson))
                {
                    FormController.deactivateFaculty(this);
                    FormController.deactivateGraduateStudent(this);
                    FormController.deactivateUndergraduateStudent(this);
                    FormController.activateChairperson(this);
                    FormController.deactivateAddButtons(this);
                    txtFacultyDepartment.Text = ((FacultyMember)p).getFacultyDepartment();
                    cbFacultyRank.Text = ((FacultyMember)p).getFacultyRank();
                    txtChairPersonStipend.Text = (((FacultyChairPerson)p).getChairPersonStipend()).ToString();
                }


            }
        }

        // Display Undergrad, Faculty, or GradStudent Form Depending on Type of Object Found
        void displayRelevantFormPart(OwlMember p)
        {
            if (p.GetType() == typeof(UndergraduateStudent))
            {
                FormController.activateUndergraduateStudent(this);
                FormController.deactivateGraduateStudent(this);
                FormController.deactivateFaculty(this);
                FormController.deactivateChairperson(this);

            }
            else if (p.GetType() == typeof(GraduateStudent))
            {
                FormController.activateGraduateStudent(this);
                FormController.deactivateUndergraduateStudent(this);
                FormController.deactivateFaculty(this);
                FormController.deactivateChairperson(this);
            }
            else if (p.GetType() == typeof(FacultyMember))
            {
                FormController.deactivateGraduateStudent(this);
                FormController.deactivateUndergraduateStudent(this);
                FormController.activateFaculty(this);
                FormController.deactivateChairperson(this);
            }
            else if (p.GetType() == typeof(FacultyChairPerson))
            {
                FormController.deactivateGraduateStudent(this);
                FormController.deactivateUndergraduateStudent(this);
                FormController.deactivateFaculty(this);
                FormController.activateChairperson(this);
            }

        }//end displayRelevatnFormPart

        private void btnFind_Click(object sender, EventArgs e)
        {
            findAnItem("Find");
        }//end btnFind






        // Validates OwlID and Tries to Find It
        private bool findAnItem
            (string operationType)
        {
            bool success;
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            btnSaveEditUpdate.Enabled = false;
            if (txtTargetID.Text.Length != 9)
            {
                MessageBox.Show("Valid Owl ID required for a " + operationType + " Renter ID.",
                    "Invalid ID for " + operationType, MessageBoxButtons.OK);
                FormController.clear(this);
                txtOwlMemberID.Text = "";
                txtOwlMemberID.Focus();
                // FormController.resetForm(this);
                success = false;
            }
            else if (thisOwlMemberList.getCount() == 0)
            {
                MessageBox.Show("No Owls to " + operationType + "Try a different command.",
                    "No " + operationType + " Empty List", MessageBoxButtons.OK);
                FormController.clear(this);
                txtOwlMemberID.Text = "";
                txtOwlMemberID.Focus();
                // FormController.resetForm(this);
                success = false;
            }
            else
            {
                // Try to find and display item to process
                bool found;
                currentIndex = thisOwlMemberList.searchOwlMemberList(Convert.ToInt32(txtTargetID.Text), out found);
                if (!found)  // Display results for processing (Find, Delete, or Edit/Update)
                {
                    MessageBox.Show("Error. ID entered does not appear in the OwlList. Reenter.",
                        "ID Not Found Error on " + operationType, MessageBoxButtons.OK);
                    txtOwlMemberID.Text = "";
                    txtOwlMemberID.Focus();
                    success = false;
                }
                else
                // Verify and then display list element and DB element
                {
                    OwlMember p = thisOwlMemberList.getAnItem(currentIndex);
                    displayRelevantFormPart(p);
                    p.Display(this);
                    success = true;
                    recordsProcessedCount++;
                }  // end if-else on found
            }  // end multiple alternative if
            return success;
        }  // end findAnItem

        private void btnExit_Click(object sender, EventArgs e)
        {
            SFManager.writeToFile(thisOwlMemberList, FileName);
            this.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            FormController.clear(this);
            FormController.activateAddButtons(this);
            buttonpress = 0;
        }

        private int clickCounter = 0;

        private void btnCreateUndergraduateStudent_Click(object sender, EventArgs e)
        {
            if (clickCounter == 0)
            {
                DisplayUndergraduateStudentForm();
                //MessageBox.Show("Clickcounter = " + clickCounter);
                clickCounter++;
                //MessageBox.Show("Clickcounter = " + clickCounter);

            }
            else
            {

                OwlMember member = new UndergraduateStudent();
                    member.Save(this);
                    thisOwlMemberList.addToList(member);
                    thisOwlMemberList.displayMembers();
                SFManager.writeToFile(thisOwlMemberList, FileName);

                clickCounter += 1;

                FormController.clear(this);
                FormController.activateAddButtons(this);
            }

        }


        private void btnCreateGraduateStudent_Click(object sender, EventArgs e)
        {
            DisplayGraduateStudentForm();
            if (clickCounter == 0)
            {
                DisplayGraduateStudentForm();
                //MessageBox.Show("Clickcounter = " + clickCounter);
                clickCounter++;
                //MessageBox.Show("Clickcounter = " + clickCounter);

            }
            else
            {

                GraduateStudent member = new GraduateStudent();
                member.Save(this);
                thisOwlMemberList.addToList(member);
                clickCounter += 1;
                FormController.clear(this);
                FormController.activateAddButtons(this);
            }

        }

        private void btnCreateFaculty_Click(object sender, EventArgs e)
        {
            DisplayFacultyForm();
            if (clickCounter == 0)
            {
                DisplayFacultyForm();
               // MessageBox.Show("Clickcounter = " + clickCounter);
                clickCounter++;
               // MessageBox.Show("Clickcounter = " + clickCounter);

            }
            else
            {
                
                FacultyMember member = new FacultyMember();
                member.Save(this);
                thisOwlMemberList.addToList(member);

                clickCounter += 1;
                FormController.clear(this);
                FormController.activateAddButtons(this);
            }
        }

        private void btnCreateChairperson_Click(object sender, EventArgs e)
        {
            DisplayChairPersonForm();
            if (clickCounter == 0)
            {
                DisplayChairPersonForm();
                //MessageBox.Show("Clickcounter = " + clickCounter);
                clickCounter++;
                //MessageBox.Show("Clickcounter = " + clickCounter);

            }
            else
            {
                FacultyChairPerson member = new FacultyChairPerson();
                member.Save(this);
                thisOwlMemberList.addToList(member);

                clickCounter += 1;

                FormController.clear(this);
                FormController.activateAddButtons(this);
            }
        }

        ////Instantiates owl member
        //private void btnSave_Click(object sender, EventArgs e)
        //{
        //    //if addMode is false 
        //    if (addMode)
        //    {
        //        //was the user found
        //        bool isFound = false;
        //        thisOwlMemberList.searchOwlMemberList(Convert.ToInt32(txtOwlMemberID.Text), out isFound);
        //        //If the memberID already exists then tell them to try again
        //        if (isFound)
        //        {
        //            MessageBox.Show("A user by this TUID exist. Please enter a new TUID.");
        //            txtOwlMemberID.Text = "";
        //            txtOwlMemberID.Focus();
        //        }

        //        else
        //        {
        //            //Create Undergraduate Student
        //            if (selectedMember == 1)
        //            {
        //                UndergraduateStudent student = new UndergraduateStudent(txtOwlMemberName.Text, Convert.ToInt32(txtOwlMemberID.Text), dtpOwlMemberBirthDate.Value, Convert.ToDecimal(txtStudentGPA.Text), txtStudentMajor.Text, Convert.ToDecimal(txtUndergraduateStudentTuition.Text), Convert.ToInt32(txtUndergraduateStudentCredits), cbUndergraduateStudentYear.Text);
        //                thisOwlMemberList.addToList(student);
        //                MessageBox.Show("OwlMember Created");
        //                FormController.clear(this);
        //            }

        //            //Create Grad Student
        //            if (selectedMember == 2)
        //            {
        //                GraduateStudent gradstudent = new GraduateStudent(txtOwlMemberName.Text, Convert.ToInt32(txtOwlMemberID.Text), dtpOwlMemberBirthDate.Value, txtStudentMajor.Text, Convert.ToDecimal(txtStudentGPA.Text), Convert.ToDecimal(txtGraduateStudentStipend.Text), cbGraduateStudentDegreeProgram.Text);
        //                thisOwlMemberList.addToList(gradstudent);
        //                MessageBox.Show("OwlMember Created");
        //                FormController.clear(this);
        //            }
        //        }
        //    }


        

        private void btnEnterID_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtTargetID.Text.Length != 9)
                {
                    MessageBox.Show("Please enter an ID of 9 intergers.");
                    txtTargetID.Text = "";
                    txtTargetID.Focus();
                }
                else
                {
                    btnFind.Enabled = true;
                    btnEdit.Enabled = true;
                    btnDelete.Enabled = true;
                }
            }
            catch
            {
                MessageBox.Show("Please enter a valid 9 integers ID number");
                txtTargetID.Text = "";
                txtTargetID.Focus();
            }
        }

        private void btnFind_Click_1(object sender, EventArgs e)
        {
            bool isFound = false;
            int member2 = thisOwlMemberList.searchOwlMemberList(Convert.ToInt32(txtTargetID.Text), out isFound);
            if (isFound)
            {
                OwlMember p = thisOwlMemberList.getAnItem(member2);
                p.Display(this);
            }
            else
            {
                MessageBox.Show("No member exists with that ID");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            bool isFound = false;
            int member2 = thisOwlMemberList.searchOwlMemberList(Convert.ToInt32(txtTargetID.Text), out isFound);
            if (isFound)
            {
                OwlMember p = thisOwlMemberList.getAnItem(member2);
                p.Display(this);

                if (MessageBox.Show("Are you sure you want to delete", "", MessageBoxButtons.YesNoCancel) == DialogResult.Yes)
                {

                    thisOwlMemberList.removeFromList(p);
                    FormController.clear(this);
                    txtTargetID.Text = "";

                }
                else
                {
                    FormController.clear(this);

                }
            }
            else
            {
                MessageBox.Show("No member exists with that ID");
            }
        }

        private void btnSaveEditUpdate_Click(object sender, EventArgs e)
        {
            bool isFound = false;
            int member2 = thisOwlMemberList.searchOwlMemberList(Convert.ToInt32(txtTargetID.Text), out isFound);
            if (isFound)
            {
                OwlMember p = thisOwlMemberList.getAnItem(member2);
                
                if (MessageBox.Show("Are you sure you want to Save update", "", MessageBoxButtons.YesNoCancel) == DialogResult.Yes)
                {
                    if (p.GetType() == typeof(UndergraduateStudent)) {
                      bool one =  validator.ValidateID(txtOwlMemberID.Text);
                      bool two =  validator.ValidGPA(txtStudentGPA.Text);
                      bool three =  validator.ValidateCredits(txtUndergraduateStudentCredits.Text);
                      bool four = validator.ValidateMajor(txtStudentMajor.Text);
                      bool five = validator.ValidateName(txtOwlMemberName.Text);
                      bool six = validator.ValidateTuition(txtUndergraduateStudentTuition.Text);
                      bool seven = validator.ValidateYear(cbUndergraduateStudentYear.Text);
                        if (one == false || two == false || three == false || four == false || five == false || six == false || seven == false) 
                        {
                            txtTargetID.Focus();
                            MessageBox.Show("Go back");
                            
                        }
                        else
                        {
                            p.Save(this);
                            thisOwlMemberList.displayMembers();
                            FormController.clear(this);
                        }
                    }
                    
                    if (p.GetType() == typeof(GraduateStudent))
                    {
                        bool one = validator.ValidateID(txtOwlMemberID.Text);
                        bool two = validator.ValidGPA(txtStudentGPA.Text);
                        bool three = validator.ValidateGraduateStipend(txtGraduateStudentStipend.Text);
                        bool four = validator.ValidateMajor(txtStudentMajor.Text);
                        bool five = validator.ValidateName(txtOwlMemberName.Text);
                        bool six = validator.ValidateDegreeProgram(cbGraduateStudentDegreeProgram.Text);
                        if (one == false || two == false || three == false || four == false || five == false || six == false)
                        {
                            txtTargetID.Focus();
                            MessageBox.Show("Go back");

                        }
                        else
                        {
                            p.Save(this);
                            thisOwlMemberList.displayMembers();
                            FormController.clear(this);
                        }
                    }
                    if (p.GetType() == typeof(FacultyMember))
                    {
                        bool one = validator.ValidateID(txtOwlMemberID.Text);
                        bool two = validator.ValidateName(txtOwlMemberName.Text);
                        bool three = validator.ValidateDepartment(txtFacultyDepartment.Text);
                        bool four = validator.ValidateRank(cbFacultyRank.Text);

                        if (one == false || two == false || three == false || four == false)
                        {
                            txtTargetID.Focus();
                            MessageBox.Show("Go back");

                        }
                        else
                        {
                            p.Save(this);
                            thisOwlMemberList.displayMembers();
                            FormController.clear(this);
                        }
                    }

                    if (p.GetType() == typeof(FacultyChairPerson))
                    {
                        bool one = validator.ValidateID(txtOwlMemberID.Text);
                        bool two = validator.ValidateName(txtOwlMemberName.Text);
                        bool three = validator.ValidateDepartment(txtFacultyDepartment.Text);
                        bool four = validator.ValidateRank(cbFacultyRank.Text);
                        bool five = validator.ValidateChairpersonStipend(txtChairPersonStipend.Text);

                        if (one == false || two == false || three == false || four == false || five == false)
                        {
                            txtTargetID.Focus();
                            MessageBox.Show("Go back");

                        }
                        else
                        {
                            p.Save(this);
                            thisOwlMemberList.displayMembers();
                            FormController.clear(this);
                        }
                    }

                  
                }
                else
                {
                    FormController.clear(this);

                }

               

            }
        }
    }
}
