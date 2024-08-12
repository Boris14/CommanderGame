extends Node
class_name PlayerController

var character : Character
var target_velocity := Vector3.ZERO
	
func _physics_process(delta):
	if not character:
		pass
		 
	var direction := Vector3.ZERO
	
	if Input.is_action_pressed("move_forward"):
		direction.z -= 1
	if Input.is_action_pressed("move_back"):
		direction.z += 1
	if Input.is_action_pressed("move_left"):
		direction.x -= 1
	if Input.is_action_pressed("move_right"):
		direction.x += 1
	
	character.move_towards(direction, delta)
	
func set_character(in_character : Character): 
	character = in_character
	
