extends Node
class_name AIController

var character : Character
var nav_agent: NavigationAgent3D
	
func _physics_process(delta):
	if not character:
		return
		
	if nav_agent.is_navigation_finished():
		return
		
	var next_position = nav_agent.get_next_path_position()
	var direction = character.global_position.direction_to(next_position)
	print(direction)
	character.move_towards(direction, delta)	

func set_character(in_character : Character):
	character = in_character
	nav_agent = character.find_child("NavigationAgent3D")
	
	nav_agent.navigation_finished.connect(_on_navigation_finished)
	
	# Test the AI
	await get_tree().create_timer(2.0).timeout
	set_move_position(character.global_position + Vector3(200, 0, 200))
	
func set_move_position(position : Vector3):
	nav_agent.target_position = position

func _on_navigation_finished():
	if character:	
		character.move_towards(Vector3.ZERO, 0)
	print("Reached destination!")
