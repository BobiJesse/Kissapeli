{Total == 0:
VAR Total = 0
}
VAR WhichChoice = 0
VAR random_number = 0
VAR Time_Left = 1200
VAR DistanceLeft = 1


-> Add_Cat(Total, 0)
==Which_Cat==
{Total:
- 1: ->Full_Dialogue
- 3: ->Full_Dialogue2
- 6: ->Full_Dialogue3
- 8: ->Full_Dialogue4
- 11: ->Full_Dialogue5
- 14: ->Full_Dialogue6
 - 17: ->Full_Dialogue7
 - else: ->Random_Cat
 }
==Full_Dialogue7==
~ Total = Total + 1
You Help us! We Meow help you! 
This cat seems to need your help
->Choices7

==Choices7==
 * [Help the cat]
 -> Help_The_Cat7
 * I am in a hurry, sorry I can't spare time for you
 * [You want to help me? That's nice!]
 -> Check_The_Dialogue

- You leave the cat alone
*[Continue]
~ WhichChoice += 1
    -> END

==Check_The_Dialogue==
I quess they want to repay my kindess in some way?
-> Choices7

==Help_The_Cat7==
You decide to help this cat
*[Continue]
    -> END
    
==Full_Dialogue==
~ Total = Total + 1
Meow meooow meow!
 * [Meow?]
    -> Help_Or_Not

==Help_Or_Not==
Looks like this cat is in need of some help
I'll see if I can help it quickly

 * [Help the cat]
 * No, I don't have time for this
    -> Leave

- You decide to see if you can help the poor cat
    *[Continue]
    -> END

==Leave==
You leave the cat alone
    *[Continue]
    ~ WhichChoice += 1
-> END

==Full_Dialogue2
~ Total = Total + 1
Meoow! I Meow Meooow
You find another cat stuck in its problems
->Choices2

==Choices2==
 * [Help the cat]
 -> Help_The_Cat2
 * Sorry, I can't help you right now
 * [Did I hear what it said correctly?]
 -> Check_The_Dialogue2

- You leave the cat alone
*[Continue]
~ WhichChoice += 1
    -> END

==Check_The_Dialogue2==
No, I must have heard it wrong
Meoow! I Meow Meooow
-> Choices2

==Help_The_Cat2==
You decide to spare your time for this cat
*[Continue]
    -> END
    
 ==Full_Dialogue3==
 ~ Total = Total + 1
Will meoow meow? Meow Meooow
This cat seems to be asking for your help
->Choices3

==Choices3==
 * [Help the cat]
 -> Help_The_Cat3
 * I am in a hurry, sorry I can't spare time for you
 * [Will I what?]
 -> Check_The_Dialogue3

- You leave the cat alone
*[Continue]
~ WhichChoice += 1
    -> END

==Check_The_Dialogue3==
I'm sure I heard the cat try to speak!
-> Choices3

==Help_The_Cat3==
You decide to help this cat
*[Continue]
    -> END

==Full_Dialogue4==
~ Total = Total + 1
I need meow! Meow help meeow?
This cat seems to be asking for your help
->Choices4

==Choices4==
 * [Help the cat]
 -> Help_The_Cat4
 * I am in a hurry, sorry I can't spare time for you
 * [Am I this sleepy or are they actually speaking to me?]
 -> Check_The_Dialogue4

- You leave the cat alone
*[Continue]
~ WhichChoice += 1
    -> END

==Check_The_Dialogue4==
Are you trying to talk to me?
I need meow! Meow help meeow?
-> Choices4

==Help_The_Cat4==
You decide to help this cat
*[Continue]
    -> END
    
    
==Full_Dialogue5==
~ Total = Total + 1
meow you help? Friends meow meow.
This cat seems to be asking for your help
->Choices5

==Choices5==
 * [Help the cat]
 -> Help_The_Cat5
 * I am in a hurry, sorry I can't spare time for you
 * [I quess they are learning to speak now, huh?]
 -> Check_The_Dialogue5

- You leave the cat alone
*[Continue]
~ WhichChoice += 1
    -> END

==Check_The_Dialogue5==
You need help? Maybe I can be of assistance
-> Choices5

==Help_The_Cat5==
You decide to help this cat
*[Continue]
    -> END
    
==Full_Dialogue6==
~ Total = Total + 1
You learn meow! We meow meoow you!
This cat seems to be asking for your help
->Choices6

==Choices6==
 * [Help the cat]
 -> Help_The_Cat6
 * I am in a hurry, sorry I can't spare time for you
 * [I'm learning you said?]
 -> Check_The_Dialogue6

- You leave the cat alone
*[Continue]
~ WhichChoice += 1
    -> END

==Check_The_Dialogue6==
I might be picking up on their language instead!
-> Choices6

==Help_The_Cat6==
You decide to help this cat
*[Continue]
    -> END
    
==Random_Cat==
{&{~Meow|Meoow|Meooow|Meow meow}{~ meow| meoow| meoow|}{~ !|?|.}} {&{~Meow|Meoow|Meooow|Meow meow}{~ meow| meoow| meoow|}{~ !|?|.}}
This cat {~ {~ is asking for| is waiting for| waits for}|{~ seems|looks|appears}{~ to be asking for| to have a need for| to demand| to be in a pickle ->ChoicesR}} your help
->ChoicesR

==ChoicesR==
 * [Help the cat]
 
 -> Help_The_CatR
 
  *{random_number <= 40}[Check the time]
 I should try to make it to the store before it closes!
 ->Check_For_Time
 
 *{random_number >= 65}[Try to pet the cat]
 {~ The cat is too scared to allow you to pet it|You pet the cat. It is soft and warm|You reach your hand towards the cat, but then it scratches you! Ouch!}
 ->ChoicesR
 
 *{random_number >= 80}[{~Hello cat!|You are a cute cat!|Why did you go and get stuck little cat?|Look deeply into the cats eyes}]
 ->Cat_Talk
  *{random_number <= 20}[{~Hello cat!|You are a cute cat!|Why did you go and get stuck little cat?|Look deeply into the cats eyes}]
 ->Cat_Talk
 
 * I have somewhere to get to, sorry I can't spare time for you
 - You leave the cat alone
 *[Continue]
~ WhichChoice += 1
-> END


 
 ==Cat_Talk==
 {~Meow|...|*Hiss*|Meow (Hello, I am cat!)|{~Meow|Meoow|Meooow|Meow meow}{~ meow| meoow| meoow|}|{~Meow|Meoow|Meooow|Meow meow}|The cat seems a little aggravated}
 {~That didn't go as you planned|You expected something more|This interaction went just as you expected|Meow to you cat!|I should propably help this cat|I should propably be going}
 ->ChoicesR
 


==Check_The_DialogueR==
~ Time_Left -= Time_Left mod 1
{Time_Left <= 100:
    - I have {Time_Left} seconds left!
       It will be a close call
    -> ChoicesR
    }
    {Time_Left <= 150:
    - I have {Time_Left} seconds left!
       Okay, I'm starting to run out of time. I have to hurry soon
    -> ChoicesR
    }
    {Time_Left < 200:
    - I have {Time_Left} seconds left!
       Still got time left!
    -> ChoicesR
    }
{Time_Left <= 300:
    - I have {Time_Left} seconds left!
      I should not waste time, but I might have enough time to help this cat
    -> ChoicesR
    }
    ~ Time_Left = 1280 - Time_Left
    
    {Time_Left <= 50:
    - I have {Time_Left} minutes left!
       It will be a close call
    -> ChoicesR
    }
    {Time_Left <= 100:
    - I have {Time_Left} minutes left!
       Okay, I'm starting to run out of time. I have to hurry soon
    -> ChoicesR
    }
    {Time_Left <= 150:
    - I have 1280-{Time_Left} minutes left!
       Still got time left!
    -> ChoicesR
    }
{Time_Left <= 200:
    - I have {Time_Left} minutes left!
      I should not waste time, but I might have enough time to help this cat
    -> ChoicesR
    }

==Help_The_CatR==
~ Total = Total + 1
You decide to help this cat
*[Continue]
-> END

==Check_The_Situation==
{ shuffle:
	- I can almost see the store! 
	    -> ChoicesR
        
	- The store is not too far, but I can't waste time 
	    -> ChoicesR
	    
	
    
    }


==Check_For_Time==
{ shuffle:
	- 	*\ [Check how long until the store closes]
	    -> Check_The_DialogueR
	- 	*\ [Check how far until you reach the store]
        -> Check_The_Situation
    -   -> Time_Check
    
    }
    
 ==Time_Check==
 -  {Time_Left <= 100:
    -   *\ Helping this cat might take too long!
    -> ChoicesR
    }
    
    -  {Time_Left > 100:
    -  {Time_Left < 200:
    -   *\ I don't know if I have enough time to help this cat
    -> ChoicesR
    }
    }
    
    -   {Time_Left > 200:
    -   {Time_Left <= 300:
    -   *\ I'm sure I have enough time to help this cat
    -> ChoicesR
    }
    }
    
    -  {Time_Left > 1200:
    -   *\ Helping this cat might take too long!
    -> ChoicesR
    }
    
    -  {Time_Left < 1200:
    -  {Time_Left >= 1150:
    -   *\ I don't know if I have enough time to help this cat
    -> ChoicesR
    }
    }
    -   {Time_Left < 1150:
    -   {Time_Left >= 1000:
    -   *\ I'm sure I have enough time to help this cat
    -> ChoicesR
    }
    }
 
    
== Add_Cat(Start, x) ==
~ Total = Total + x
~ random_number = RANDOM(1, 100)

->Which_Cat
