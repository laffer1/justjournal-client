/* JustJournalController */

#import <Cocoa/Cocoa.h>
#import "JustJournal.h"

@interface JustJournalController : NSObject
{
    IBOutlet JustJournal *justjournal;
    IBOutlet NSTextField *password;
    IBOutlet NSTextField *username;
}
- (IBAction)login:(id)sender;
@end
