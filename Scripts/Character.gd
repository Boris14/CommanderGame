extends CharacterBody3D
class_name Character

# How fast the player moves in meters per second.
@export var max_speed = 6.0
# The downward acceleration when in the air, in meters per second squared.
@export var acceleration := 10.0
@export var fall_acceleratin := 75.0
@export var friction := 10.0
@export var controller_scene : PackedScene

var state := GlobalEnums.CharacterState.IDLE
var target_velocity := Vector3.ZERO

func _ready():
	if controller_scene:
		var controller = controller_scene.instantiate()
		add_child(controller)
		if controller.has_method("set_character"):
			controller.set_character(self)

func _physics_process(delta):
	if not is_on_floor():
		velocity.y = velocity.y - fall_acceleratin * delta
	else:
		velocity.y = 0

func change_state(new_state : GlobalEnums.CharacterState):
	if state == new_state:
		pass
	
	match(new_state):
		GlobalEnums.CharacterState.IDLE:
			$AnimationPlayer.stop()
			$Armature/Skeleton3D.show_rest_only = true
		GlobalEnums.CharacterState.RUNNING:
			$Armature/Skeleton3D.show_rest_only = false
			$AnimationPlayer.play("Running")
			
	state = new_state

func move_towards(direction : Vector3, delta : float):
	direction = direction.normalized()

	if abs(direction) > Vector3.ZERO:
		velocity = velocity.lerp(direction * max_speed, delta * acceleration)
	else:
		velocity = velocity.lerp(Vector3.ZERO, delta * friction)
	
	if abs(direction) > Vector3.ZERO:
		change_state(GlobalEnums.CharacterState.RUNNING)
		$Armature.basis = Basis.looking_at(direction)
	else:
		change_state(GlobalEnums.CharacterState.IDLE)

	move_and_slide()
