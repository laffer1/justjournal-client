// This file was generated by Dashcode from Apple Inc.
// DO NOT EDIT - This file is maintained automatically by Dashcode.
function setupParts() {
    if (setupParts.called) return;
    setupParts.called = true;
    CreateInfoButton('info', { frontID: 'front', foregroundStyle: 'white', backgroundStyle: 'black', onclick: 'showBack' });
    CreateGlassButton('done', { text: 'Done', onclick: 'showFront' });
    CreateGlassButton('btnPost', { onclick: 'myPost', text: 'Button' });
    CreateText('text', { text: 'subject' });
    CreateText('text1', { text: 'username' });
    CreateText('text2', { text: 'password' });
    CreateText('text3', { text: 'JustJournal.com' });
    CreatePopupButton('lstsecurity', { options: unescape('[[%27Public%27%2C %272%27%2C true]%2C [%27Friends%27%2C %271%27]%2C [%27Private%27%2C %270%27]]'), rightImageWidth: 16, leftImageWidth: 1 });
    CreateText('text4', { text: 'security' });
}
window.addEventListener('load', setupParts, false);