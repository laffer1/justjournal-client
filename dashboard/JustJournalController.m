#import "JustJournalController.h"

@implementation JustJournalController

- (IBAction)login:(id)sender
{
    NSString *user;
    NSString *pass;
   
    /*user = [NSString alloc];
    [user 
    [user stringValue:[username stringValue]];*/
    user = [username stringValue];
    pass =     [password stringValue];
        
    [JustJournal login:user Password:pass];
}

@end
