extends Node3D

@export var model: Node3D
@export var sensitivity: float = 0.5
@export var smoothness: float = 10.0
@export var invert_drag: bool = true

var _is_dragging: bool = false
var _target_rotation_y: float = 0.0

func _ready():
	if model:
		_target_rotation_y = model.rotation.y

func _unhandled_input(event: InputEvent):
	# Check for Mouse Click
	if event is InputEventMouseButton:
		if event.button_index == MOUSE_BUTTON_LEFT:
			_is_dragging = event.pressed

	# Check for Mouse Movement while dragging
	if _is_dragging and event is InputEventMouseMotion:
		var direction = -1 if invert_drag else 1
		# Relative movement is in pixels, so we convert to radians
		var rotation_amount = deg_to_rad(event.relative.x * sensitivity * direction)
		_target_rotation_y -= rotation_amount

func _process(delta: float):
	if not model:
		return
		
	var current_rotation = model.rotation
	# lerp_angle ensures the model rotates the shortest distance between angles
	current_rotation.y = lerp_angle(current_rotation.y, _target_rotation_y, delta * smoothness)
	model.rotation = current_rotation