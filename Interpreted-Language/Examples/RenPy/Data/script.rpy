character_a = character('character_id')
character_b = character('other_character_id')
room_a = room('room_name')

label label_name:
    character_a '...'
    character_a 'Hey.'
    character_a sprite 10
    
    jump other_label

label other_label:
    character_b 'hey'

character_a 'Hello!'
character_a 'Testing escaped quotes.'
character_a 'I\'m a character.'
character_b sprite 25 # testing integers.

jump label_name

character_a '------- end of script. ---------'