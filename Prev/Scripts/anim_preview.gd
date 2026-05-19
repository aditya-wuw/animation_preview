extends CanvasLayer

const SMUG_CAT = r"
	   /\                ______
	  /  \______________/      \
	 /                          \
	/    O                 O     \____
   |            \    /                |
   |   @         \/\/       @        /
   |                                /
   |_______________________________/  Smug Cat's
"

@export var model: Node3D
@export var btn_scene: PackedScene
@export var anim_container: VBoxContainer
@export var search_bar: LineEdit

var anims: PackedStringArray
var anim_player: AnimationPlayer
@onready var anim_count: Label = $anim_count

func _ready():
	print(SMUG_CAT)
	search_bar.text_changed.connect(_on_search_terms)
	anim_player = model.get_node("AnimationPlayer")
	anims = anim_player.get_animation_list()
	display_animations(anims)

func display_animations(display_list: Array):
	#print(display_list)
	for child in anim_container.get_children():
		if child is Button:
			child.queue_free()
	anim_count.text = str(display_list.size())
	for anim_name in display_list:
		var new_btn = btn_scene.instantiate() as Button
		new_btn.text = anim_name
		new_btn.pressed.connect(play_animation.bind(anim_name))
		
		anim_container.add_child(new_btn)

func _on_search_terms(terms: String):
	# print(terms)
	var filtered = []
	for a in anims:
		if terms == "" or a.to_lower().contains(terms.to_lower()):
			filtered.append(a)
			
	display_animations(filtered)

func play_animation(anim_name: String):
	anim_player.play(anim_name)
