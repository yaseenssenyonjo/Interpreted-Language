character_a = character('character_name')
room_a = room('room_name')
# character_b = character(24)

label label_name:
        character_a '...'
    character_a 'Hey.'
        character_a sprite 10
    
    jump nested_label

label nested_label:
    character_b 'hey'

character_a 'Hello!'
character_a 'Testing escaped quotes.'
character_a 'I\'m a character.'
character_a sprite 25 # testing integers.

jump label_name
        # todo: fixed: a single line with a tab on it throws errors as the parser cannot find a group to match it.
character_a 'continues after jumping.'