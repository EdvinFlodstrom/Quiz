Journal
============

2023-10-18
-------------
#### 4219 Frågesport Net Core - A och B.
I have finished part A and B, so you can now manually enter regular and 
Multiple Choice Single Answer (MCSA) quiz questions. These are saved in a list,
and when you play the game, a random card is picked, read, and discarded. 
Your score is displayed while you play, and you get a message based on your score at the end.
The message is simple: either you get a perfect score, all wrong or something in between.
I didn't struggle particularly much with anything, but I had to stop and think for a bit
when making the class for the MCSA questions. Using inheritance, I created something I think works fine.
I used the following links throughout part A and B:
* https://stackoverflow.com/questions/21345005/add-multiple-items-to-a-list
* https://stackoverflow.com/questions/19318430/c-sharp-select-random-element-from-list
* https://learn.microsoft.com/en-us/dotnet/api/system.string.split?view=net-7.0
* https://stackoverflow.com/questions/983030/type-checking-typeof-gettype-or-is

Using a few various techniques, I managed to make the program fairly robust. You can't enter
a number when the answer requires text, and you can't enter text when the answer requires a number.
Questions that require multiple words also work fairly well, where the program splits the required answer 
into separate strings and checks to make sure that every word in the answer appears at least once in the 
submitted answer. For example, the question "What is Eyjafjallajökull, and where is it located?", where 
the answer required is "volcano iceland", will require the user to type in anything that has the words
"volcano" and "iceland" somewhere in it. This method is fairly simple, but it comes with some drawbacks.
However, for simple reasons such as this, it works well enough. After this, I intend to add more questions,
make it possible for the player to add their own questions, and make sure that even if you misspell something
slighly, the answer will still pass.

2023-11-14
--------------
#### 4131 WPF Filhantering
I continued working on the quiz program, both with a UI and with file reading. The program now reads the questions from a file,
and it works perfectly, I think. Each question is read, and then a new question is created and added to a list depending 
on the type of question. I am currently trying to add a method that resets the deck, as currently you can only play the quiz
once per run of the program. Since the deck removes each card from itself after the card is played, the deck runs out of
cards after each play. I'm trying to update the deck variable in UserInterface with `deck = new Deck();`, but it doesn't seem
to updpate deck in Quiz despite UserInterface calling Quiz's constructor with the deck as an argument. This process involves
a few steps, so I'm not certain exactly what is going wrong where. The obvious problem is that deck in Quiz doesn't update 
accordingly, though.

2023-11-17
--------------
#### 4131 WPF Filhantering
Deck now updates each time the quiz is initiated. I fixed this by moving the instance creation of the class Deck to Quiz from 
UserInterface.

I've continued with the program for a while now, and it's been a smooth process. Up until now, anyway. So, I solved the problem
I've been trying to fix for the last 15 minutes, but I am still going to note it down here because of how dumb my mistake was.
When trying to create a string with all the combiened options entered for an MCSA question, I had a string that could look 
like this: "1,2,3,4,5,". Note the "," at the end, it's what I was trying to get rid of. So I went to a Stack Overflow page, 
this one: https://stackoverflow.com/questions/3573284/trim-last-character-from-a-string. It recommended me to try 
`combinedMCSAOptionsString.Remove(combinedMCSAOptionsString.Length - 1);`, which I did. It didn't solve it though, so I 
apparently channeled my inner fool and removed "- 1". This, of course, throws an error because the Remove functions tries to
remove something that is outside the string in question. But, because I have a try-catch statement surrounding the code, 
including the code snippet I mentioned earlier, the catch statement catches the error. Since the catch statement in this case
is meant to stop you from entering the same option twice (I'm not sure if I should be doing it this way), the code doesn't
log an error for the message. Suffice to say, I didn't understand what the error was. After a while of using the debugger, I
came across the root of the issue (removing "- 1", this was not very smart), and I swiftly fixed the issue. To summarize: I
made a rather dumb mistake and it took me a ludicrous amount of time to fix it for what it was. So now I'll get back to what
I was trying to do at the start, removing the last ',' in the string I mentioned earlier. 

And it's fixed. I should probably check when methods actually modify the value they're called upon and not, because that's
what was confusing me here. I simply added `combinedMCSAOptionsString = ` before the 
`combinedMCSAOptionsString.Remove(combinedMCSAOptionsString.Length - 1);`. So now it looks like this: 
`combinedMCSAOptionsString = combinedMCSAOptionsString.Remove(combinedMCSAOptionsString.Length - 1);`.

2023-11-21
-------------
#### 4131 WPF Filhantering
Rather than continuing with adding more functions to the program, I've started polishing it a bit more. To start out,
I removed all try-catch statements as they were unnecessary and caused confusion. Rather than using try{} to convert
strings to integers, I'm now using `int.TryParse(answer, out intAnswer)`, which is Microsofts recommended way of 
doing it. It doesn't throw exceptions, so it makes the code more clear. I've also started separating the class
UserInterface into two, one containing only the interface itself, and the other, AcitonHandler, containing all the
functions of the UI. ActionHandler will be the class that saves data in variables and adds them together to create 
questions and some other things. Despite the fact that I've only yet created a method for creating questions, 
not one for deleting or modifying them, it still takes a lot of work to separate the current UI class. In the end,
though, the program should be much more clear and easily readable, which is preferable.

Some time later, I've now split UserInterface into two classes, more or less. Some of the functions that UserInterface
previously took care of, such as creating the string for the question to be added and calling a method in FileManager,
are now handled by ActionHandler instead. Although I didn't manage to completely and thoroughly separate the two, 
at least it's a little better now. As it is right now, UserInterface asks a few questions, saves the answers, 
and sends the answers as arguments to a method in ActionHandler that uses the parameters to concatenate them into
a single string, which is then sent as an argument to a method in FileManager. It took almost three hours, but
the code is now a lot better written.

And now the program can write to the 'quiz_questions.txt' file as a way to save user-made questions. These questions
appear as soon as you start a new quiz, be it right after adding a question or after restarting the program. It works
as intended, in other words. During this lesson, I used the following links:
* https://github.com/karlsson0214/WpfFileDemo/blob/master/WpfFileDemo/MainWindow.xaml.cs
* https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/type-testing-and-cast
* https://stackoverflow.com/questions/3160127/whats-the-point-of-as-keyword-in-c-sharp
* https://stackoverflow.com/questions/7042314/can-i-check-if-a-variable-can-be-cast-to-a-specified-type#7042384
* https://stackoverflow.com/questions/1722964/when-to-use-try-catch-blocks
* https://stackoverflow.com/questions/2344411/how-to-convert-string-to-integer-in-c-sharp

2023-11-22
-------------
#### 4131 WPF Filhantering
I've now also implemented question removing. The currently available options are now: 1. Take the quiz, 2. Add a
quesiton to the quiz, and 3. Remove a quesiton from the quiz. I was pondering several different approaches when it
came to removing questions from the quiz, and eventually used one that reads all the current lines from the quiz
and adds them to a string, unless the question being read has the same index as the index of the question the user
wants to remove (the user is asked to input the number of the question they want to remove). I then do this: 
`File.WriteAllText(pathAndFileName, newStringOfQuestions);`. Since there is already a document at pathAndFileName,
the file is overwritten with the contents of newStringOfQuestions, which is a single string containing all the
questions, answers and options of all the questions. In hindsight, I wonder if the string will ever get too large, 
but that's a problem for a later time, I think. It works, so that's a win in my books.
* https://stackoverflow.com/questions/33490890/write-the-data-to-specific-line-c-sharp
* https://stackoverflow.com/questions/668907/how-to-delete-a-line-from-a-text-file-in-c
* https://stackoverflow.com/questions/44235689/how-to-overwrite-a-file-if-it-already-exists
* https://learn.microsoft.com/en-us/dotnet/api/system.io.file.writealltext?view=net-8.0&redirectedfrom=MSDN#System_IO_File_WriteAllText_System_String_System_String_

A little bit later, now you can also modify questions in the quiz. For once, I daresay I handled it fairly well. 
Rather than adding a billion new methods for finding the correct question and extracting it and modifying it, I instead
rewrote a few methods, including the one that removes a question. So the method in UserInterface that modifies a 
question is only three lines long, and it's because it uses the methods for creating a question and removing
a question. So what happens when you modify a question is the following: 1. All questions are listed from 1-x. 
2. You choose which question to modify. 3. You add a new question (but the question is never added, rather, a string
with the question is returned for later use). 4. FileManager's `RemoveOrModifyQuestion(int numberOfQuestion, string modifiedQuestion = "")`
is called, with modifiedQuestion not being equal to "". 5. A streamreader reads the entire .txt file and saves
each line in a single string, EXCEPT for if the index of the line being read is the same as the one you chose
to modify. If that is the case, the program checks that `(modifiedQuestion != "")`. If this evaluates to true, 
the program adds the question you added earlier (I mentioned that it is saved for later use) to the string instead.
The program then continues the same way as when you delete a question: it overwrites the contents of the file with 
the string containing the new questions. In the end, the .txt file looks the exact same except the modified question
is replaced with a completely new question that you write yourself. When writing this, though, I thought of something.
If you try to enter the same answer for two different options in an MCSA (Multiple Choice Single Answer) question,
the program tells you "no" and returns to the menu. Problem is, the question you chose to modify is removed and 
not replaced with anything. I'll have to fix this real quick.

It didn't take many minutes to fix the mentioned above issue. I simply separated the 
`RemoveOrModifyQuestion(int numberOfQuestion, string modifiedQuestion = "")` method into a few more steps, so it
now looks like this:
```
    int numberOfQuestionToModify = Convert.ToInt32(VerifyAnswer(numberOfQuestions));
    string modifiedQuestion = CreateQuestion(true);

    if (modifiedQuestion != "")
    {
        fm.RemoveOrModifyQuestion(numberOfQuestionToModify, modifiedQuestion);
    }
    else
    {
        Console.WriteLine("Creation of new question failed. No change was made to the chosen question. Please try again.");
    }
```
It functions the exact same, but this version has an added failsafe to make sure that nothing goes wrong if you
fail to create a new question. I think the program as a whole is fully functional now, and I haven't found any 
unfixed bugs so far. I added ten base questions (I'm aware they aren't on GitHub, but they can be found 
commented out in previous versions in the Deck class) that can be removed or modified, which should suffice
for what this program is, I think. I'm not sure how file management would work should anyone download these files
from GitHub, but at it all works locally for me. So this base program should be done now, but I'm probably going 
to implement some API related features in a bit.

Bug spotted! It took me a grand total of two minutes to find a problem in my program. Got to be a new record, I think.
So anyhow, I chose to add a new MCSA question and added the same option twice. This, as indended, does not work. No
question was added, but a message saying it was, was still written out. It won't take long to fix.

Bug fixed, program work. I added a simple if statement to verify that when adding a question, the return value isn't
"". The only time it is equal to "" is when the question wasn't added, and so a message does not need to be written.
I only know of one way to fail the creation of a question, and that is to add the same option twice for MCSA questions.
For this scenario, I've already written a specific message, and as such, I believe no general message has to be
written for failing the creation of a question. Now it may be possible to input a value so large that the program
can't handle it and crashes, but I don't think I'll want to account for that possibility here. The indended
functions are functional, and I think that should suffice for now. I did test something along those lines though,
and when inputting a ridiculously large number (as many 9's as was allowed) for the "How many cards are in a 
regular card deck?" question, I got a message saying "Please input numbers, not letters". Odd result, but at least 
it didn't crash.

Quick fix later, I've added a .ToLower() in Quiz when checking the inputted answer to make sure that you still get 
a right answer if you for example type "iceland" when the answer is "Iceland". 
Makes it a little more user friendly, I think.

2023-11-24
--------------
#### 4131 WPF Filhantering
This morning, I thought of something. I thought: "What if you try to add a question that already exists in the quiz 
to the quiz?". Answer: it works. Some fifteen minutes later, now it doesn't. In the FileManager class, 
I added an if-statement to the AddQuestion method, like this:
```
if (questionCardString.ToLower().Contains(item[1].ToLower()))
{
    return false; //Quiz does not allow you to add a question that already exists.
}
```
I also changed the return type from void to bool, so that ActionHandler can check that if the return value is false,
write a message to the console stating that the question already exists. I also added trimming to the question and
answer you input for adding questions, so that no unnecessary spaces are included in the beginning or end of a 
question.
* https://learn.microsoft.com/en-us/dotnet/standard/base-types/trimming

2023-11-24
-------------
#### WPF Filhantering
It would seem that I have yet to separate the UI from the rest of the program to a sufficent degree. So I've started
fixing that, and I have only one class left to fix, I think. Problem is, it's the first or second class I wrote, so
at the time I wasn't thinking that I would eventually change the user interface. Alas, now I am. So, time to clean up
the Quiz class, I suppose. Thing is, it is BAD. REALLY bad. I think I'll actually just ditch most of the methods
in Quiz and move it all over to UserInterface instead, since so much of it is `Console.WriteLine` or `Console.ReadLine`.
I'll keep some simple calculations like how many answers you've gotten correct or something similar, but so much of
what's currently in Quiz does NOT belong there.

I'm not sure if I can even salvage this class. I'm thinking of simply starting over with the class Quiz, to get it 
right from the start. 

I'm also thinking of another thing. Namely, either straight up removing Quiz.cs and moving all its functions and 
methods to the class ActionHandler, or restructuring a few things a tad and treating Quiz like some sort of 
action handler of its own.

I'm going to try moving the instance creation of the class Quiz from ActionHandler to UserInterface. I'll also
largely adjust all the functions in Quiz, so that it acts more like ActionHandler does. In the end, it's probably
the smoothest fix I can think of.

2023-11-25
------------
#### WPF Filhantering
I'm getting an interesting error, here. When setting the return type to `List<QuestionCard>` for the Run() method in Quiz, 
I get an error stating `Inconsistent accessibility: return type 'List<QuestionCard>' is less accessible than method
Quiz.Run()`. I noticed that if I change the class from public back to internal, this error disappears. I'm unsure
how I should deal with this error, since the class may have to be public in order for other user interfaces (such as
the GUI that will be implemented soon) to have to have some form of access to the class Quiz.

Some two minutes later, the problem is no more. When I was testing how to fix it, I tried changing the access modifier
for another method in Quiz, one that returns a bool value. No error occured. It then took me about two seconds to 
realize that I can't return a list of QuestionCards from Quiz, because the class QuestionCard was still internal. So 
after switching QuestionCard from internal to public, it's working.
* https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/access-modifiers

Alright, quiz is back up and fully functional. The quiz is, functionally, the exact same as before, with one exception.
For questions such as "What is Eyjafjallajökull, and where is it located?", you can now answer something like "2".
In other words, the quiz doesn't stop you from inputting a single number for an answer that requires text.
There are benefits with the quiz stopping such answers, but there are also some benefits with this simpler approach 
as well, so I'll leave it as is for now. Anyhow, about 80% of what was in Quiz is now in ActionHandler, minus
all the unnecessary `Console.WriteLine()` and `Console.ReadLine()`. All those are now in UserInterface, so the 
program should be user interface-independent now. Since the user interface in usage has to call some methods in a
specific order for the quiz to work, I suppose it could've been done better. But I also suppose that is always the 
case. The quiz works, and now the UI is properly separated from the rest of the program. It took me a while to 
salvage the atrocity that was the Quiz class, but it is done.

2023-11-29
-------------
#### WPF Filhantering
I've been trying to implement a GUI for a bit now, but I was struggling with getting the project to start. I kept 
getting an error in XamlReader.cs (I haven't written this class). The Xaml wasn't able to parse, it seems. It took
me a while to find the cause of the error, but I've now found it. Adding `ActionHandler handler = new ActionHandler();`
to MainWindow.xaml.cs is the problem. Thing is, I don't know why this throws an error. I've tried running both 
the "FrågesportNetCore" project and the GUI project at the same time, but it still throws an error (I have a
`using FrågesportNetCore;` reference in the class that causes the error. This same error is thrown regardless of
where I instantiate the ActionHandler class, in the constructor or not. ActionHandler is also public. The error
is also thrown regardless of whether ActionHandler has a constructor or not.

Some testing later, I've made a little progress. I implemented a button that, when you press it, should write
instructions. It starts by instantiating ActionHandler. Then the program breaks. Same error as before, but on a 
different line and in EventRoute.cs instead of XamlReader.cs. It can't load FrågesportNetCore.exe because the 
.exe or .dll file is invalid. Same as before, even if I run both projects at the same time, trying to instantiate a 
class from FrågesportNetCore in WPF_GUI_Question instantly throws an error. At least I got the button working, so 
that's some measure of progress at least.

I'm not really getting anywhere with debugging this little problem, so I'll paste the details below for some reason:

```
System.BadImageFormatException
  HResult=0x800700C1
  Message=Could not load file or assembly 'C:\Users\04edfl12\OneDrive - Stenungsunds Kommun\Programmering\FrågesportNetCore\WPF_GUI_Question\bin\Debug\net8.0-windows\FrågesportNetCore.exe'. Format of the executable (.exe) or library (.dll) is invalid.
  Source=WPF_GUI_Question
  StackTrace:
   at WPF_GUI_Question.MainWindow.Btn1_Click(Object sender, RoutedEventArgs e) in C:\Users\04edfl12\OneDrive - Stenungsunds Kommun\Programmering\FrågesportNetCore\WPF_GUI_Question\MainWindow.xaml.cs:line 32
   at System.Windows.EventRoute.InvokeHandlersImpl(Object source, RoutedEventArgs args, Boolean reRaised) in System.Windows\EventRoute.cs:line 123
   at System.Windows.UIElement.RaiseEventImpl(DependencyObject sender, RoutedEventArgs args) in System.Windows\UIElement.cs:line 6297
   at System.Windows.Controls.Primitives.ButtonBase.OnClick()
   at System.Windows.Controls.Button.OnClick()
   at System.Windows.Controls.Primitives.ButtonBase.OnMouseLeftButtonUp(MouseButtonEventArgs e)
   at System.Windows.RoutedEventArgs.InvokeHandler(Delegate handler, Object target) in System.Windows\RoutedEventArgs.cs:line 201
   at System.Windows.EventRoute.InvokeHandlersImpl(Object source, RoutedEventArgs args, Boolean reRaised) in System.Windows\EventRoute.cs:line 123
   at System.Windows.UIElement.ReRaiseEventAs(DependencyObject sender, RoutedEventArgs args, RoutedEvent newEvent) in System.Windows\UIElement.cs:line 6272
   at System.Windows.RoutedEventArgs.InvokeHandler(Delegate handler, Object target) in System.Windows\RoutedEventArgs.cs:line 201
   at System.Windows.EventRoute.InvokeHandlersImpl(Object source, RoutedEventArgs args, Boolean reRaised) in System.Windows\EventRoute.cs:line 123
   at System.Windows.UIElement.RaiseEventImpl(DependencyObject sender, RoutedEventArgs args) in System.Windows\UIElement.cs:line 6297
   at System.Windows.UIElement.RaiseTrustedEvent(RoutedEventArgs args) in System.Windows\UIElement.cs:line 2710
   at System.Windows.Input.InputManager.ProcessStagingArea() in System.Windows.Input\InputManager.cs:line 546
   at System.Windows.Input.InputProviderSite.ReportInput(InputReport inputReport) in System.Windows.Input\InputProviderSite.cs:line 52
   at System.Windows.Interop.HwndMouseInputProvider.ReportInput(IntPtr hwnd, InputMode mode, Int32 timestamp, RawMouseActions actions, Int32 x, Int32 y, Int32 wheel) in System.Windows.Interop\HwndMouseInputProvider.cs:line 797
   at System.Windows.Interop.HwndMouseInputProvider.FilterMessage(IntPtr hwnd, WindowMessage msg, IntPtr wParam, IntPtr lParam, Boolean& handled) in System.Windows.Interop\HwndMouseInputProvider.cs:line 350
   at System.Windows.Interop.HwndSource.InputFilterMessage(IntPtr hwnd, Int32 msg, IntPtr wParam, IntPtr lParam, Boolean& handled) in System.Windows.Interop\HwndSource.cs:line 1140
   at MS.Win32.HwndWrapper.WndProc(IntPtr hwnd, Int32 msg, IntPtr wParam, IntPtr lParam, Boolean& handled)
   at MS.Win32.HwndSubclass.DispatcherCallbackOperation(Object o)
   at System.Windows.Threading.ExceptionWrapper.InternalRealCall(Delegate callback, Object args, Int32 numArgs)
   at System.Windows.Threading.ExceptionWrapper.TryCatchWhen(Object source, Delegate callback, Object args, Int32 numArgs, Delegate catchHandler)
   at System.Windows.Threading.Dispatcher.LegacyInvokeImpl(DispatcherPriority priority, TimeSpan timeout, Delegate method, Object args, Int32 numArgs)
   at MS.Win32.HwndSubclass.SubclassWndProc(IntPtr hwnd, Int32 msg, IntPtr wParam, IntPtr lParam)
   at MS.Win32.UnsafeNativeMethods.DispatchMessage(MSG& msg)
   at System.Windows.Threading.Dispatcher.PushFrameImpl(DispatcherFrame frame)
   at System.Windows.Application.RunDispatcher(Object ignore)
   at System.Windows.Application.RunInternal(Window window)
   at WPF_GUI_Question.App.Main()
```

Lot of details in this one, I think. I came across an answer on StackOverflow 
(https://stackoverflow.com/questions/323140/system-badimageformatexception-could-not-load-file-or-assembly-from-installuti)
suggesting to change the processor architecture for AnyCPU projects from auto. Alas, this did not fix my problem.

2023-12-01
--------------
#### WPF Filhantering
Right so the UI was not properly separated from the rest of the program. But now it is! I think. The console UI is now
located in a new project, "FrågesportNetFramework", which is essentially a copy of "FrågesportNetCore". I removed
the latter, since it has been replaced. Kind of funny, in a way, how this project as a whole has changed. Anyhow
I had a lot of trouble fixing dependencies and stuff with the three projects, but it works now. It seems that 
"FrågesportNetCore" targeting .NET framework version 4.8 was not very good for creating dependencies with projects that
target .NET framework version 8. So "FrågesportNetFramework" targets .NET framework version 8, and now it all works.
I'll continue working on the GUI, which can access all functions and classes of the new project QuizLibrary, which is
where all the classes that the user doesn't directly interact with are located.
* https://stackoverflow.com/questions/38042130/what-is-a-circular-dependency-and-how-can-i-solve-it
* https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/compiler-messages/cs0246?f1url=%3FappId%3Droslyn%26k%3Dk(CS0246)
* https://stackoverflow.com/questions/48555467/cant-add-reference-to-visual-c-sharp-console-app-in-visual-studio
* https://stackoverflow.com/questions/38148128/how-do-i-reference-a-net-framework-project-in-a-net-core-project

I'm not sure why I keep thinking I've properly separated the UI-related classes from the rest, to a  SUFFICIENT degree. 
Clearly I haven't, yet. I was about to implement the whole "choose your option" in the GUI, and I'm only now realizing 
how far from adequate the UserInterface class is. I should probably tacke it immediately to make my life a little
simpler. And I should probably stop thinking I've ever completely separated the UI from the rest of the project. At 
this point, it's getting somewhat ridiculous how many times I've said I've completed it only to realize how incorrect
I've been *every time*...

2023-12-05
-------------
#### WPF Filhantering
With the quiz.cs class, I feel like I sort of shot myself in the foot with the poor separation of UI and such. 
UserInterface, though, this is something else. I hardly know what it should look like with a proper separation, so I 
don't know if I'm even doing it right. I *think* it's getting a little better, maybe? It's taking far longer than it
has any right to, though. Henceforth, I will be sure never to make these mistakes again, as it is an atrocious process 
to separate an existing class into part-UI, and part-not-UI like this.

I've successfully got the quiz working, but I'm having issues with making question creation efficient. I'm struggling
with separating it in a good way, since there needs to be various numbers of `Console.ReadLine()` based on what you
answer. This leads to issues when I need to call certain methods in a certain order a certain amount of times. My
main idea is to return the name of a method (or maybe just the method itself) from InterfaceHandler to UserInterface
to make it easier to call the correct methods in the correct order after each `Console.ReadLine()`. I don't know how
to do this, though. I don't even know if it's possible (or good practice for that matter). I've researched 
delegates and asynchronous methods, but I'm unsure of whether one of these will actually provide what I need.
* https://stackoverflow.com/questions/25191512/async-await-return-task
* https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/await
* https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/delegates/how-to-combine-delegates-multicast-delegates

2023-12-06
--------------
#### WPF Filhantering
I'm growing more certain that I, with my current skill, am unable to *fully* separate the UI from the rest. I'm making
progress, sure, but not enough. I guess I'll have to accept an inadequate separation in this case, but I will do my
best at separating them still. From what I can tell so far, UserInterface is looking to be around half of it's 
previous length in code, so it's not like I've done nothing I suppose.

2023-12-08
-----------------
#### WPF Filhantering
I've continued working on UserInterface. I'm almost done with it, but then I realized that the MCSA questions are broken.
Again. The options aren't displaying, but I'll fix it in due time. It probably won't take too long, I think. I've also
made some progress with adding options for removing/modifying questions, and it's going well enough.

Update: I don't know what I'm doing. Somewhere, sometime, I changed something. Not a great combination of events. So 
anyhow the questions aren't randomized, ever. I have a method, Draw(), in Deck, for this. I must've used it at some
point, since I recall the questions being randomized before I started trying to fix the atrocity that was UserInterface.
So I also need to fix the questions not being randomized. Not much fun in seeing them in the same order all the time,
I think.

2023-12-09
--------------
#### WPF Filhantering
I've fixed the questions not being randomized. It didn't take that long, but it was an interesting path I had to 
take to get there. It showed that this project is getting a little complex, maybe too complex for what it is. Anyhow,
this is the path I followed: 1. UserInterface calls InterfaceHandler.PerformAction(). 2. InterfaceHandler calls 
ActionHandler.PrepareQuiz. 3. ActionHandler calls Quiz.Run. Quiz creates a new instance of Deck, randomizes
all the cards and returns them as a List of QuestionCards. This was the first part, where I saw how I was randomizing
the questions. Second part: 1. UserInterface calls InterfaceHandler.PerformAction(). InterfaceHandler calls
ActionHandler.GetAllQuestions(). And here the problem was. I'm not sure why or when, but at some point when implementing
the method GetAllQuestions(), I chose to get the questions from FileManager.ReadFile() instead of from the *already 
existing list containing all the questions*. I really don't know what I was thinking there. Anyhow, since the 
randomization of questions happens in Quiz, getting the questions from FileManager.ReadFile() will cause them to be
in the same order as they are in the .txt document, every time. Quick fix later, the questions are now being randomized.

I've also found the reason for the MCSA questions not displaying their answers. Similar to the previous question, I
did something at one point and seem to have forgotten about it later. As it is right now, when taking the quiz,
all the questions get added to a list, twice. So if there are ten questions, the list will contain twenty questions.
The MCSA questions added by one method do contain options, while they don't in the other.

Two minutes later, problem solved. The debugger really is quite helpful. So anyway what I had to do was move the
calling of the RunQuiz() method in InterfaceHandler, and clear listOfAllQuestions instead of setting it to 
ActionHandler.GetAllQuestions().

Now that I'm working on targeting questions currently in the quiz, I realized that the questions not being randomized
above was actually not the problem, the problem was simply how I was adding them to a list of questions. So I changed
some of that, and it's now possible to get the questions as randomized or not. This will make it a little simpler
to target questions when you want to remove or modify them.

At last, the console user interface is now working again. It took a gruesome amount of time to repair it all after this
brutal separation, but I dare not assume that it is wholly separated just yet. At least it's better now, I think. 
UserInterface and InterfaceHandler include over 50% more code than what UserInterface had before, but hopefully it'll save
me a lot of time and effort when I implement the GUI, which I will continue doing tomorrow. There was a lot of debugging
involved in this whole part, and doing it without the debugger would have been an absolute nightmare. Being able to step
into methods and follow variable assignment and operations was certainly a saving grace here.
* https://stackoverflow.com/questions/7745509/how-to-specify-default-value-for-list-parameter-in-a-c-sharp-function
* https://stackoverflow.com/questions/4630444/how-to-skip-optional-parameters-in-c

2023-12-12
-------------
#### WPF Filhantering
At first, I was trying to fix some things with MCSA questions in the GUI. I was trying to use patterns and Regex and a
bunch of stuff that was unnecessary. I then discussed the project with my teacher, and came to the conclusion that I've 
been handling some things incorrectly. We set up a basic structure, and I think it's now time to actually get this
project going. I've been handling the QuestionCard and MCSACard classes wrong, since when for example comparing the 
inputted answer with the correct answer, I haven't accessed the card's actual correct answer but rather a copy of
that value. Some unnecessary things, in other words. I will clean up all of that, then fix the GUI (which will be simpler
with these fixes) and then implement more features. Just commenting out two object variables that did not adhere to
these principles brought about 10 errors. It's time to actually get this project fixed, definitively.

Fascinating, how much I fixed by only removing code. All I did was move code, remove code, and combine InterfaceHandler
with ActionHandler. And now the program works a lot better. The command-line UI still works great (I only had to 
adjust some minor things to make it work), and the implementation of the GUI is going well, too. Before I changed
the things I mentioned above, I was struggling with displaying each option for MCSA questions on a different button. 
I was trying to use patterns and regexes and it was *not* going well. But after the changes, it took me a mere minute
to get it working perfectly. I suppose this will be a lesson always to keep a clear idea of what I am doing, and not
implement a bunch of useless workarounds for no obvious reason.

When checking the answer that the user inputs in the GUI, I ran into a problem with checking the MCSA answer. The 
regular questions where you type in your answer worked fine, but the MCSA ones did not. It took me a minute or two to
realize, but when I was debugging the project I realized that sending the `[button].Content.ToString()` sends the 
content of the button (obviously), while InterfaceHandler expects an answer in the range 1-5. No major issue, I simply
changed `[button].Content.ToString()` to `"[number of the button in question]"`.

2023-12-13
------------
#### WPF Filhantering
I've run into a curious issue. When checking the answer in the GUI, I always get an incorrect. Using the debugger, I 
quickly saw that the question was comparing with a different question than is displayed. It was working earlier, so 
I must've changed something recently. I know for a fact it has to do with indexes, I just need to find where.

As expected, 1) Solving the issue took about 20 seconds, and 2) It had to do with indexes. The issue was simply that I
moved the incrementation of an index up, instead of down, in the code. So the quiz can now be played in a GUI as well,
it is fully functional and can be replayed both in a command-line UI and a GUI now. I of course have to add the remaining
four actions to the GUI as well, some of which will be a little more troublesome than others. Like a true professional,
I copy pasted the first reply from StackOverflow regarding one of these actions. In fairness, the code was literally
just `Application.Current.Shutdown();`. 
* https://stackoverflow.com/questions/2820357/how-do-i-exit-a-wpf-application-programmatically

Problem spotted! Not sure where it came from this time. In the command-line UI, when I want to choose a question to 
target, for removal or modification, the questions come in a randomized order. However, if I target question number 3
(they are lined up with a number, 1-cards.Count), the program targets the third question in the file, which is the
third question when they are *not* randomized. I know what the issue is, I just have to think of a nice way to solve it.

The problem was a little more complex than I was expecting, but I think I know what to do now. The user picks a question
to target, from a randomized list. The number of the question is then used to target a question. I was trying to 
convert this number of the question from the randomized list to the number of the same question in the sorted list. 
The problem I was trying to fix is that I couldn't get IndexOf to work. My code looked like this: 
`numberOfQuestion = quiz.ListOfSortedQuestionCards().IndexOf(questionCards[Convert.ToInt32(numberOfQuestion)]).ToString();`.
It took me a while to realize (I think I know what is wrong anyway), but the problem is that I'm comparing two different
instances of the class QuestionCard. They are identical, but they are still different instances of the class. I assume
this is why it doesn't work.

Bingo! Comparing the questions of the questions instead of the questions themselves (questionCard.Question instead of just
questionCard) did the trick.

2023-12-14
----------------
#### WPF Filhantering
It wasn't quite as easy as it seemed at first, to fix the issue I ran into yesterday. But now it is fixed, so there's 
that. I changed the code to: 
```
foreach (QuestionCard item in quiz.ListOfSortedQuestionCards())
    {
        if (item.Question == questionCards[Convert.ToInt32(numberOfQuestion)].Question)
        {                    
            numberOfQuestion = quiz.ListOfSortedQuestionCards().IndexOf(item).ToString();
            break;
        }
    }
```
Not too complicated, but it works. Back to the GUI now.